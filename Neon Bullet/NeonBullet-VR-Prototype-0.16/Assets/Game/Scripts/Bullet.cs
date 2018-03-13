using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    [HideInInspector] public float damage;
    public GameObject bulletHitEnemyEffect;
    //public GameObject bulletSolidEnemyEffect;

    [HideInInspector] public Vector3 bulletDirection;
    [HideInInspector] public float bulletSpeed;

    //public AudioSource bulletFiredSound;
    //public AudioSource bulletNormalSound;

    //private float bulletFiredSoundPitchOriginal;
    //private float bulletNormalSoundPitchOriginal;

    //public AudioClip bulletFiredSound;
    //public AudioClip bulletNormalSound;

    private GameManager gameManager;

    private float step;

    private void Awake()
    {
        //blletFiredSoundPitchOriginal = bulletFiredSound.pitch;
        //bulletNormalSoundPitchOriginal = bulletNormalSound.pitch;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        Destroy(this.gameObject, 2f);
    }

    //public GameObject bulletDissolveEffect;

    private void Update()
    {
        if (gameManager.inRedemption)
        {
            Destroy(this.gameObject);
        }

        //bulletFiredSound.pitch = Mathf.Lerp(bulletFiredSound.pitch, bulletFiredSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);
        //bulletNormalSound.pitch = Mathf.Lerp(bulletNormalSound.pitch, bulletNormalSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);

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
