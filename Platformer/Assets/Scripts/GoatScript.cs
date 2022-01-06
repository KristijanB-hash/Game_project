using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatScript : MonoBehaviour
{
	[SerializeField]
	Transform castPoint;

	[SerializeField]                                       //TUKA
	Transform castPoint2;

	[SerializeField]
	Transform player;

	[SerializeField]
	float agroRange;

    [SerializeField]
	float moveSpeed;

	bool isFacingLeft;
	private bool isAgro=false;
    

    Rigidbody2D rb;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        /*float distToPlayer=Vector2.Distance(transform.position, player.position);                  //nacin da proverime dalecina od 1 do drugo neso

        if(distToPlayer < agroRange)                             
        {
        	ChasePlayer();
        }
        else                                                       
        {
        	StopChasingPlayer();
        }
        */

        if(CanSeePlayer(agroRange))
        {
        	isAgro=true;
        	ChasePlayer();                                                //so ovoa te glea samo na levo,ama ne gore
        }
    }

    bool CanSeePlayer(float distance)
    {
    	bool val=false;                                  //val=value
    	float castDist=distance;

    	if(isFacingLeft==true)
    	{
    		castDist= -distance;
    	}

    	Vector2 endPos= castPoint.position +Vector3.right * castDist;
    	RaycastHit2D hit= Physics2D.Linecast(castPoint.position, endPos, 1<< LayerMask.NameToLayer("Action")); //tretoto nesto vo linecast e sto bara

    	if(hit.collider != null)                             //ima najdeno nesto
    	{
    		if(hit.collider.gameObject.CompareTag("Player"))
    		{
    			val =true;
    		}

    		else
    		{
    			val=false;
    		}
    	}
    	   	return val;
    }

     void ChasePlayer()
    {
    	if(transform.position.x < player.position.x)
    	{
    		rb.velocity=new Vector2(moveSpeed, 0);                                //na desno go brka
    		transform.localScale=new Vector2(-1,1);                               //da se flipne spritot
    		isFacingLeft=false;
    	}
    	else
    	{
    		rb.velocity=new Vector2(-moveSpeed, 0);                               //na levo go brka
    		transform.localScale=new Vector2(1,1);                               //da se flipne spritot
    		isFacingLeft=true;
    	}
    }

     void StopChasingPlayer()
    {
    	isAgro=false;
    	rb.velocity=new Vector2(0, 0);
    }
}
