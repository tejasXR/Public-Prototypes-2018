using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage;
    public GameObject bulletHitEnemyEffect;
    public GameObject bulletSolidEnemyEffect;
    //public GameObject bulletDissolveEffect;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet" || collision.gameObject.tag == "Solid")
        {
            Instantiate(bulletHitEnemyEffect, transform.position, transform.rotation);
            Destroy(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        Instantiate(bulletHitEnemyEffect, transform.position, transform.rotation);
    }
}
