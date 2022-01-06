using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
	private float timeBtwShots;
	public float startTimeBtwShots;

	public GameObject projectile;


    void Start()
    {
    	timeBtwShots=startTimeBtwShots;
        
    }

    void Update()
    {
    	if(timeBtwShots<=0)
    	{
    		
    		GetComponent<Animator>().SetBool("Attacking",true);
    		HelperScript.Instance.DelayedExecution(.3f, ()=>Instantiate(projectile,transform.position,Quaternion.identity));
    		timeBtwShots=startTimeBtwShots;
    	}
    	else{
    		timeBtwShots-=Time.deltaTime;
    		GetComponent<Animator>().SetBool("Attacking",false);
    	}
        
    }
}
