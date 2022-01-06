using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Playermovement player;          //gr8 primer za koristenje promenliva od dr skripta..... player tuka go kreirame

    public float startTimeBtwAttack;
    private float timeBtwAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;

    private int attackCount=0;
    private bool canAttack=true;
    private int clickCount=0;

    public int damage;

    public Animator attackAnim;

    void Update()
    {
        if (timeBtwAttack <= 0)
        {
            attackCount = 0;
            clickCount = 0;
        }


            if(Input.GetKeyDown("c") && player.isGrounded)    //gr8 primer za koristenje promenliva od dr skripta
            {
                clickCount += 1;
                chainAttack(attackCount);

                timeBtwAttack = startTimeBtwAttack;
        }


        timeBtwAttack -= Time.deltaTime;

    }

    private void OnDrawGizmosSelected()                                                  //samo za polesno da vizuelizirame 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    public void NextAttack()
    {
        canAttack = true;
        attackCount += 1;

        if (clickCount >= 2)
        {

            if (attackCount == 1)
            {
                FindObjectOfType<AudioManager>().Play("slash2");
                attackAnim.SetTrigger("AttackAnim2");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().health -= damage;
                }
            }

            else if (attackCount == 2)
            {
                FindObjectOfType<AudioManager>().Play("slash3");
                attackAnim.SetTrigger("AttackAnim3");

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().health -= damage;
                }
            }

            clickCount = 0;
        }
    }
    private void chainAttack(int attackCount)
    {
        Collider2D[] enemiesToDamage;

        if(attackCount==0 && clickCount==1)
        {
            FindObjectOfType<AudioManager>().Play("slash1");
            attackAnim.SetTrigger("AttackAnim1");                           //AttackAnim1 e triger setnat vo animation tab-ot

            enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<EnemyHealth>().health -= damage;                              //povikuvame health os skriptata EnemyHealth i j anamaluvame koga ke bide damaged enemy
            }
        }

    }
}
