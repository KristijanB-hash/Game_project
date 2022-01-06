using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	public float speed;
	public float lifetime;

	private Transform player;
	private Transform target;     
	private Vector2 dir;
	private float timer;
	public LayerMask whatIsGround;

	void Start()
	{
		timer=0;
		player=GameObject.FindGameObjectWithTag("Player").transform;
		dir=(player.position-transform.position).normalized;                           //player.pos e kaj se naogja igracot, a transfor e kaj se naogja projectile
                                                                                       //ako nema normalized brzinata ke e zavisna od kolku e daleku player od projectile
	}

	void Update()
	{
		timer+=Time.deltaTime;
		transform.Translate(dir*speed*Time.deltaTime);
		if(timer>=lifetime)
		{
			DestroyProjectile();             //koa ke pomine vreme da se despawne
		}

		if (Physics2D.OverlapCircle(transform.position, 0.1f, whatIsGround))
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)         //projectile se destroynuva
	{
		if(other.CompareTag("Player"))
		{
			DestroyProjectile();
		}
	}

	void DestroyProjectile()
	{
		Destroy(gameObject);
	}
}
