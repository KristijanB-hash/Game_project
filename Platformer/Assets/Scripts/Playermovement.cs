using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovement : MonoBehaviour
{
    public ParticleSystem dust;
    public Animator animator;

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private bool facingRight = true;   //za na kade ke e svrten (default e desno)

    public bool isGrounded;           //prasaj tuka
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsValue;
    private float jumpTimeCounter;
    public float jumpTime;
    bool isJumping;

    bool isTouchinFront;
    public Transform frontCheck;
    public bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public float dashForce;
    private bool isDashing;
    public float dashTime;
    private int dashReady = 1;
    private float currentDashTime;
    private float timeBTWDashes=1;

    public GameObject dashEffectRight;
    public GameObject sparcleEffect;
    public GameObject sparcleEffect2;
    public float effectTime;
    public float effectTimeSparcles;





    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        //rb = GetComponent<Rigidbody2D>();                          OVA e originalot

        extraJumps = extraJumpsValue;
        currentDashTime = 0;
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);   //dvizenje levo i desno


        animator.SetFloat("Speed", Mathf.Abs(moveInput));



        if (facingRight == false && moveInput > 0)              //ako karakterot se dvizi,amoa ne desno togas napravi flip
        {
            Flip();
        }

        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

        if (!isDashing)
        {
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        }

        else if (Input.GetKey("right"))                           //vo ovoj del veke se pravi dash samo se odreduva na kade
        {
            FindObjectOfType<AudioManager>().Play("dash");
            FindObjectOfType<AudioManager>().Play("bells");
            rb.velocity=Vector2.right*dashForce;
            
        }

        else if (Input.GetKey("left"))
        {
            FindObjectOfType<AudioManager>().Play("dash");
            FindObjectOfType<AudioManager>().Play("bells");
            rb.velocity = Vector2.left * dashForce;

        }
    }

    void Update()
    {

        if (isGrounded == true)
        {
            extraJumps = extraJumpsValue;                //se polnat jumps koga ke se stapne na zemja
            dashReady = 1;                             //dashot se popolnuva koga ke se stapne na zemja
            animator.SetBool("isJumping", false);
            animator.SetBool("isGrounded", true);
        }
        else if(isGrounded==false && isDashing==true)   
        { 
            animator.SetBool("isDashing", true);
            animator.SetBool("isGrounded", false);
        }        
        else { animator.SetBool("isJumping", true); }   

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);         //jumpTimeCounter proveruva kolku imas 
                                                                                                       //vreme da skokas uste i koa
        if (extraJumps>=1 && Input.GetKeyDown("up"))                                       //ke se potrosi isJumping ide false                          
        {                                                                                            //i nemozes vise da skokas
            animator.SetTrigger("TakeOff");            
            CreateDust();
            isJumping = true;                                                                          //se resetnuva koa ke se vretis na zemja
            jumpTimeCounter = jumpTime;                                                                //ama ne na dzid
            rb.velocity = Vector2.up * jumpForce;                                                      //na dzid si ima svoj jump
            extraJumps -= 1;
        }

        if (Input.GetKey("up") && isJumping == true)
        {
            FindObjectOfType<AudioManager>().Play("JumpSound");

            animator.SetBool("isJumping", true);           
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp("up"))
        {
            isJumping = false;
        }

        isTouchinFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if (isTouchinFront == true && isGrounded == false && moveInput != 0)
        {
            wallSliding = true;
        }

        else
        {
            wallSliding = false;
        }

        if (wallSliding == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));          //perhaps this will fix wallslide  (comment this line to remove wall slide)
            dashReady = 1;                                                                                                    //dashot se popolnuva koga ke se dopre wall
            extraJumps = extraJumpsValue;                                                                                     //se polnat jumps koa ke se dopre dzid

            animator.SetBool("isWallSliding", true);
        }

        else
        {
            animator.SetBool("isWallSliding", false);
        }

        if (Input.GetKeyDown("up") == true && wallSliding == true)
        {
            wallJumping = true;
            Invoke("setWallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveInput, yWallForce);
        }

        if (Input.GetKeyDown("down") && isGrounded == false)
        {
            rb.gravityScale = 2;
        }
        if (Input.GetKeyUp("down"))
        {
            rb.gravityScale = 1;
        }

        timeBTWDashes += Time.deltaTime;
        if (Input.GetKeyDown("z") && dashReady == 1 && rb.velocity.x != 0 && timeBTWDashes>=0.1)       //vtoriot del e za da nemoze da pravis dash anim vo mesto... alos ne menja timeBTWDashes       
        {
            StartCoroutine(Dash());
            dashReady = 0;
        }

        /*if(Input.GetKeyDown(immaterial))                                             
        {
            GetComponent<CapsuleCollider2D>().isTrigger = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;                              //ova tuka e neso za fazeing through stuff
        }
        */


    }

    void Flip()                             //flipni go karakterot na levo
    {
         if (isGrounded == true && !wallSliding)
         {
             CreateDust();                             
         }   
        
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void setWallJumpingToFalse()
    {
        wallJumping = false;
    }

    IEnumerator Dash()
    {
        GameObject tempEfekt;
        GameObject sparcleEfekt1;
        GameObject sparcleEfekt2;

        tempEfekt = Instantiate(dashEffectRight, dashEffectRight.transform.position, dashEffectRight.transform.rotation);
        if(!facingRight)
        {
            Vector3 scaler = tempEfekt.transform.localScale;
            scaler.x *= -1;
            tempEfekt.transform.localScale = scaler;
        }

        sparcleEfekt1 = Instantiate(sparcleEffect, sparcleEffect.transform.position, sparcleEffect.transform.rotation);
        sparcleEfekt2 = Instantiate(sparcleEffect2, sparcleEffect2.transform.position, sparcleEffect2.transform.rotation);

        tempEfekt.SetActive(true);
        sparcleEfekt1.SetActive(true);
        sparcleEfekt2.SetActive(true);

        timeBTWDashes = 0;
        isDashing = true;
        GetComponent<Animator>().SetBool("isDashing", isDashing);                  //Dash
        yield return new WaitForSeconds(dashTime);                                 //kolku vreme dashnuva
        isDashing = false;
        GetComponent<Animator>().SetBool("isDashing", isDashing);

        yield return new WaitForSeconds(effectTime);
        {
            Destroy(tempEfekt);
        }
        yield return new WaitForSeconds(effectTimeSparcles);
        {
            Destroy(sparcleEfekt1);
            Destroy(sparcleEfekt2);
        }

    }

    void CreateDust()                     //pravi dust effect
    {
        dust.Play();
    }

    public IEnumerator Knockback(float knocbackDuration, float knockbackPowerY, Vector3 knockbackDirection, float knockbackPowerX)          //OVOJ ENUMERATOR e za knockback i se koristi vo HeartsAnsBullets skriptata
    {
        float timer = 0;

        while (knocbackDuration > timer)
        {
            timer += Time.deltaTime;
               rb.AddForce(new Vector3(knockbackDirection.x * knockbackPowerX, knockbackDirection.y * -knockbackPowerY, transform.position.z));
            yield return 0;
        }
       
       // rb.AddForce(new Vector3(knockbackDirection.x * knockbackPowerX, knockbackDirection.y * -knockbackPowerY, transform.position.z), ForceMode2D.Impulse);

    }

    public void playWalkingSound()
    {


        FindObjectOfType<AudioManager>().Play("walking");
    }

    public void stopWalkingSound()
    {
        FindObjectOfType<AudioManager>().Stop("walking");
    }

}

