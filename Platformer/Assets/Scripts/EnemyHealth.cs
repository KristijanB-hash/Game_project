using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int health;
	public float secondsBeforeDying;
	private bool isEnemyDead=false;

	//public GameObject bloodEffect;                                            

	public void TakeDamage(int damage)
	{
		health-=damage;
	}

	void Update()
	{
		if(health<=0 && !isEnemyDead)
		{
			StartCoroutine(Wait());                                                                      //povikuvame wait() korutinata         OVA E BAD PRACTICE .....NE STAVAJ CORUINA U UPDATE cuz moze da se povikuva sekoj frame ako ne pazis
			isEnemyDead = true;

			//Instantiate(bloodEffect, transform.position, Quaternion.identity);                         //se koristi za malku cool da izglea deth na wizards
		}
	}

	void OnTriggerEnter2D(Collider2D col)                         //ako udri bullet gubi zivot
	{
		if (col.gameObject.tag == "Bullet")
		{
			health--;
		}
	}

	IEnumerator Wait()                                                                                  //pravime wait() korutina
    {
		yield return new WaitForSeconds(secondsBeforeDying);
		Destroy(gameObject);
	}
}
