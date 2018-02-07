using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [HideInInspector] public float damage;
    public GameObject bulletHitEnemyEffect;
    public GameObject bulletSolidEnemyEffect;

    [HideInInspector] public Vector3 bulletDirection;
    [HideInInspector] public float bulletSpeed;

    private float step;

    //public GameObject bulletDissolveEffect;

    private void Update()
    {
        //step = bulletSpeed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, bulletDirection, step);
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Solid")
        {
            //Instantiate(bulletHitEnemyEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Solid")
        {
            //Instantiate(bulletHitEnemyEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        Instantiate(bulletHitEnemyEffect, transform.position, transform.rotation);
    }
}
