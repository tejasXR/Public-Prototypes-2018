using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float enemyHealth;
    public Player playerController; //Whole of player object
    private float enemyGiveHealth; //Amount of health enemy gives to player after it is destroyed
    public GameObject player; //cameraEye object

    private Vector3 playerDirection;

    public float enemyBulletFireRate; //bullets fires per second
    public float enemyBulletTimer;
    public float enemyBulletSpeed;

    public GameObject enemyBulletPrefab;
    public Transform enemyBulletSpawn;

    public Vector3 targetPosition;

    private Ray ray;


    //public GameObject bulletPrefab;

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        enemyGiveHealth = enemyHealth;

        //targetPosition = 

        Vector3 randomDirection = new Vector3(Random.Range(-1, 1), Random.Range(.5f,1), Random.Range(-1, 1));
        ray = new Ray(playerController.transform.position, randomDirection);

        targetPosition = ray.GetPoint(8);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        playerDirection = player.transform.position - transform.position;

        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }

        //Look at player
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = rotation;

        if (enemyBulletTimer < enemyBulletFireRate)
        {
            enemyBulletTimer += Time.deltaTime;

        }
        else
        {
            enemyBulletTimer = enemyBulletFireRate;
            Fire();
        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            var damage = collision.gameObject.GetComponent<Bullet>().damage;
            enemyHealth -= damage;
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        playerController.playerHealth += enemyGiveHealth;

    }

    void Fire()
    {

        if (enemyBulletTimer == enemyBulletFireRate)
        {
            //Instantiate bullet
            var bullet = Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * enemyBulletSpeed;

            // Destroy the bullet after 2 seconds
            Destroy(bullet, 2.0f);

            enemyBulletTimer = 0;
        }
    }
}
