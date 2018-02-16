using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [HideInInspector] public float damage;
    public GameObject bulletHitEnemyEffect;
    //public GameObject bulletSolidEnemyEffect;

    [HideInInspector] public Vector3 bulletDirection;
    [HideInInspector] public float bulletSpeed;

    public AudioSource bulletFiredSound;
    public AudioSource bulletNormalSound;

    //public AudioClip bulletFiredSound;
    //public AudioClip bulletNormalSound;

    private GameManager gameManager;

    private float step;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        bulletFiredSound.pitch = 1f + Random.Range(-.05f, .05f);
        bulletNormalSound.pitch = 1f + Random.Range(-.05f, .05f);

        bulletFiredSound.Play();
        bulletNormalSound.Play();

        Destroy(this.gameObject, 2f);
    }

    //public GameObject bulletDissolveEffect;

    private void Update()
    {
        if (gameManager.inRedemption)
        {
            Destroy(this.gameObject);
        }
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
