using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    //public float damage;
    public GameObject bulletHitEffect;
    //public GameObject bulletSolidEnemyEffect;
    //public GameObject bulletDissolveEffect;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //print(rb.velocity);
        print(transform.rotation);
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

            rb.velocity *= -1;

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
