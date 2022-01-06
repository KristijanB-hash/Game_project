using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    private float timeBtwShield;
    public float startTimeBtwShield;

    public Transform shieldPos;
    public float shieldRange;
    public LayerMask whatIsBullet;                              //btw bullets se so tag spikes



    void Update()
    {
        if (timeBtwShield <= 0)
        {

            if (Input.GetKey("x"))
            {
                Debug.Log("se klika kopceto");
                Collider2D[] bulletsToEliminate = Physics2D.OverlapCircleAll(shieldPos.position, shieldRange, whatIsBullet);
                for (int i = 0; i < bulletsToEliminate.Length; i++)
                {
    
                    Destroy(bulletsToEliminate[i].gameObject);
                    
                }
            }
            timeBtwShield = startTimeBtwShield;
        }
        else
        {
            timeBtwShield -= Time.deltaTime;
        }
    }

/*        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(collision.gameObject);
        }
*/
    private void OnDrawGizmosSelected()                                                  //samo za polesno da vizuelizirame 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(shieldPos.position, shieldRange);
    }
}
