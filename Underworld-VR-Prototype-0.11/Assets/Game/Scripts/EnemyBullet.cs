using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    //public float damage;
    public GameObject bulletHitEffect;
    //public GameObject bulletSolidEnemyEffect;
    //public GameObject bulletDissolveEffect;

    public float damage;
    private Rigidbody rb;
    private Vector3 transformStart;
    public GameObject enemyParent;
    public GameManager gameManager;
    public Material defelectedBulletMat;
    public bool isRedemptionBullet;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //print(rb.velocity);
        //print(transform.rotation);
        transformStart = transform.position;

        //Finds enemy that fired bullet
        //enemyParent = 

        //Vector3 diff = 

    }

    private void Update()
    {
        if(gameManager.inRedemption && !isRedemptionBullet)
        {
            Destroy(this.gameObject);
;        }

        if (gameManager.inRound && isRedemptionBullet)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Solid" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Shield" || collision.gameObject.tag == "Bullet" || (collision.gameObject.tag == "Enemy" && this.gameObject.tag == "DeflectedBullet"))
        {
            Instantiate(bulletHitEffect, transform.position, transform.rotation);

            if (this.gameObject. tag == "DefelctedBullet" && collision.gameObject.tag == "EnemyBullet")
            {
                Destroy(collision.gameObject);
            }

            //print(collision.gameObject.tag);

            Destroy(this.gameObject);
        }

        /*if (collision.gameObject.tag == "Enemy" && this.gameObject.tag == "DeflectedBullet")
        {
            Instantiate(bulletHitEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }*/


        /*if (collision.gameObject.tag == "Sword" && this.gameObject.tag != "DeflectedBullet")
        {
            //var localVel = transform.InverseTransformDirection(rb.velocity);
            //rb.velocity = localVel.z;

            //rb.velocity *= -1;

            //Get the nearest enemy

            //float deflectAccuracy = .03f;

            Vector3 bulletDirection = enemyParent.transform.position - transform.position;

            if (!enemyParent.gameObject.activeInHierarchy)
            {
                bulletDirection = -transform.position;
            }

            transform.rotation = Quaternion.LookRotation(bulletDirection);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = bulletDirection * 3f;

           

            //bulletDirection.x += Random.Range(-deflectAccuracy, deflectAccuracy);
            //bulletDirection.y += Random.Range(-deflectAccuracy, deflectAccuracy);
            //bulletDirection.z += Random.Range(-deflectAccuracy, deflectAccuracy);


            
            this.gameObject.tag = "DeflectedBullet";
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
        //Instantiate(bulletHitEffect, transform.position, transform.rotation);
    }

    void BulletDestroy()
    {
        Instantiate(bulletHitEffect, transform.position, transform.rotation);
        Destroy(this.gameObject);
    }
}
