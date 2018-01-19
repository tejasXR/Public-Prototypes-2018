using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    //public float damage;
    public GameObject bulletHitEffect;
    //public GameObject bulletSolidEnemyEffect;
    //public GameObject bulletDissolveEffect;

    private Rigidbody rb;
    private Vector3 transformStart;
    public GameObject enemyParent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //print(rb.velocity);
        print(transform.rotation);
        transformStart = transform.position;

        //Finds enemy that fired bullet
        //enemyParent = 

        //Vector3 diff = 

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Solid" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Bullet")
        {
            Instantiate(bulletHitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }

        if (collision.gameObject.tag == "Sword")
        {
            //var localVel = transform.InverseTransformDirection(rb.velocity);
            //rb.velocity = localVel.z;

            //rb.velocity *= -1;

            //Get the nearest enemy

            //float deflectAccuracy = .03f;

            var bulletDirection = enemyParent.transform.position - transform.position;

            //bulletDirection.x += Random.Range(-deflectAccuracy, deflectAccuracy);
            //bulletDirection.y += Random.Range(-deflectAccuracy, deflectAccuracy);
            //bulletDirection.z += Random.Range(-deflectAccuracy, deflectAccuracy);


            transform.rotation = Quaternion.LookRotation(bulletDirection);
            rb.velocity = bulletDirection * 2f;
            //transform.rotation = collision.gameObject.transform.rotation;

            //rb.velocity = -rb.velocity * 5f;
            //transform.position = Vector3.Reflect(transform.position, Vector3.up);

            //Vector3 rot = transform.rotation.eulerAngles;
            //rot = new Vector3(rot.x, rot.y + 180, rot.z);
            //transform.rotation = Quaternion.Euler(rot);

            //rb.velocity = transform.forward * 5f;

        }

        /*if (collision.gameObject.tag == "Solid")
        {
            Instantiate(bulletSolidEnemyEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }*/
    }

    private void OnDestroy()
    {
        Instantiate(bulletHitEffect, transform.position, transform.rotation);
    }
}
