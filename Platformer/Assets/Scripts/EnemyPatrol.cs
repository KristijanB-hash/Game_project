using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float walkSpeed;

    private float Timer = 0;                   

    [HideInInspector]
    public bool mustPatrol;
    public Rigidbody2D rb;

   // public LayerMask groundLayer;
    public Collider2D bodyCollider;

    void Start()
    {
        mustPatrol = true;
    }

    void Update()
    {
        if (mustPatrol == true)
        {
            Patrol();
            Timer = 0;              
        }
        else
        {
            Timer += Time.deltaTime;
            if(Timer >= 1)
            {
                mustPatrol = true;
            }
        }
        
    }

    void Patrol()
    {
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        rb.velocity = Vector2.zero;
        walkSpeed *= -1;
       // mustPatrol = true;
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.tag.Equals("Spikes") && !col.tag.Equals("Bullet"))                     //za sea na ovoa funkcionira
        Flip();
    }
   
}