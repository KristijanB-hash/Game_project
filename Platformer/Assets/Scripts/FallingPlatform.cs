using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
	Rigidbody2D rb;

    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name.Equals("player")){
        	//Invoke("DropPlatform",0.5f);                    //ako sakas da padne platformata     0.5f e za kolku vreme(pola sekunda)
        	Destroy (gameObject,0.5f);                        //ako sakas da ja snema platformata
        }
    }

    /*void DropPlatform()
    {
    	rb.isKinematic=false;
    }
    */
}
