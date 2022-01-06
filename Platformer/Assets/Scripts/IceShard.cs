using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceShard : MonoBehaviour                                                         //OVA E ZA ICE SUN
{
    private Vector2 moveDirection;
    private float moveSpeed;
    public LayerMask whatIsGround;


    private void OnEnable()                         //projectile se destroynuva posle 3 sekund
    {
        Invoke("Destroy", 3f);
    }
    void OnTriggerEnter2D(Collider2D other)         //projectile se destroynuva ako udri player
    {
        if (other.CompareTag("Player"))
        {
            Destroy();

        }
    }

    void Start()
    {
        moveSpeed = 2f;
    }

    void Update()
    {
        if( Physics2D.OverlapCircle(transform.position, 0.1f, whatIsGround))
        {
            Destroy(gameObject);
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

}
