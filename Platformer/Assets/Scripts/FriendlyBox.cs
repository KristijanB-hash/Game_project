using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyBox : MonoBehaviour
{
    public float walkSpeed;

    [HideInInspector]
    public bool mustPatrol;
    public Rigidbody2D rb;

    public LayerMask groundLayer;
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
        }
    }

    void Patrol()
    {
        if (bodyCollider.IsTouchingLayers(groundLayer))
        {
            Flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Equals("player"))
        {
            Destroy(gameObject, 0.5f);                        //ako sakas da ja snema platformata
        }
    }
}
