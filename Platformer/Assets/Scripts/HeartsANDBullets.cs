using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;   

public class HeartsANDBullets : MonoBehaviour
{
	public int health;
	public int numOfHearts;

	public Image[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;

	public Animator animator1;                             //se vika animator1 deka animator go imame veke vo Playermovement skriptata i nejkam da gi mesam

	private Playermovement player;                                //OVOA


    void Start()                                                 //OVOJ Start
    {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Playermovement>();
    }
    void Update(){

		if(health<=0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);       //ako izgubi zivoti da pocne od novo scenata
        }

		if(health>numOfHearts){
			health=numOfHearts;
		}

		for(int i=0;i<hearts.Length;i++)                                //kod used in case i change my mind i sakam da imas sprites of hearts vo gorno kjose
		{

			if(i<health){
				hearts[i].sprite=fullHeart;
			} else {
				hearts[i].sprite=emptyHeart;
			}

			if(i<numOfHearts){
				hearts[i].enabled=true;

			} else{
				hearts[i].enabled=false;
			}
		}

	}

	void OnTriggerEnter2D(Collider2D col)                         //ako udri spikes gubi zivot
    {
        if(col.gameObject.tag=="Spikes")
        {
            //Debug.Log("Lost health");
            health=health-30;                                     //insta death(30 e big number)
		}

		if(col.gameObject.tag=="HurtsThePlayer")
        {
			//Debug.Log("Lost health");
			health -- ;
			animator1.SetTrigger("TookDmg");

	//		StartCoroutine(player.Knockback(0.2f, 2f, player.transform.position,20f));                   //OVAA LINIJA KOD e za knockback
		}
    }

}
