using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    Vector2 mousePos;
    public Camera cam;

    public float bulletForce = 20f;


    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);                            

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;                      //Leki code
        diff.Normalize();                                                                                             //more Leki code

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;                                                    //also Leki code
        

        Vector2 lookDir = mousePos - gameObject.GetComponent<Rigidbody2D>().position;                          //we make a vector that points from our player to our mouse
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;                                  

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);        //we make the bullet exist
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z -50);                              //we make the bullet face the direction we want(P.S. koa ke zamenis so nov sprite samo menjaj ja brojkata na kraj. Trial nad error e)
        Rigidbody2D rb= bullet.GetComponent<Rigidbody2D>();

        rb.AddForce(lookDir * bulletForce, ForceMode2D.Impulse);
    }
}
