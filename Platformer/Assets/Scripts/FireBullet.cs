using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour                                       //OVA E ZA ICE SUN
{

    [SerializeField]
    private int bulletAmount = 10;


    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;                         //od koj do koj agol da se pukaat kursumite

    private Vector2 bulletMoveDirection; 

    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletAmount;                //proporcionalno delenje na bullets
        float angle = startAngle;

        for(int i=0; i<bulletAmount+1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);        //gadni matematiki
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletPool.bulletPoolInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<IceShard>().SetMoveDirection(bulDir);

            angle += angleStep;

        }

    }

}
