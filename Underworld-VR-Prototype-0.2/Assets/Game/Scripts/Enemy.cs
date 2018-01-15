using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Define Basic Player Variables
    public Player playerController; //Whole of player object
    public GameObject player; //cameraEye object
    private Vector3 playerDirection; //Direction towards the player

    //Define Basic Enemy Variables
    public float enemyHealth;
    public float enemyGiveBullets; //Amount of bullets enemy gives to player after it is destroyed

    //Enemy Attack Behavior
    public float enemyBulletFireRate; //bullets fires per second
    public float enemyAttackTimer; //timer to know when to attack next
    public float enemyBulletSpeed; //speed of enemy bullet
    public Vector3 randomDirection; //random direction needed for enemy gun accuracy
    private Ray ray; //ray needed for enemy gun accuracy
    public float enemyAccuracy; //enemy accuray in % form
    public Vector3 enemyBulletDirection; //direction of the enemy bullet when generated

    public GameObject enemyBulletPrefab; //the enemy bullet gameObject
    public GameObject explosionPrefab; //the explosion effect when destroyed
    public Transform enemyBulletSpawn; //the object for where to spawn the enemy bullet

    //Enemy Movement Behavior
    public Vector3 targetPosition; //the position that the enemy is constantly moving to
    public float enemyMoveTimer; //time in seconds before enemy switches places
    private Vector3 velocity = Vector3.zero; //velocity needed for smoothDamp movement

    void Start()
    {
        //Define player variables for enemy prefab
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        //Initiate RandomPosition to go to a random position when first created
        RandomPosition();
    }

    void Update()
    {
        //Always move towards targetPosition
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f, 5f);

        //Look at player
        playerDirection = enemyBulletDirection - transform.position;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20f * Time.deltaTime);

        //If the attack timer equals the fire rate, then attack, else keep increasing the timer
        if (enemyAttackTimer < enemyBulletFireRate)
        {
            enemyAttackTimer += Time.deltaTime;
        }
        else
        {
            enemyAttackTimer = enemyBulletFireRate;
            enemyBulletFireRate = Random.Range(.7f, 2f);
            Fire();
        }

        //Constantly decrease the moveTimer and if it hits zero, move
        enemyMoveTimer -= Time.deltaTime;
        if (enemyMoveTimer <= 0)
        {
            RandomPosition();
            enemyMoveTimer = Random.Range(5, 10);
        }

        //If helath hits zero, destroy enemy
        if (enemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    //If player bullet hits enemy
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            var damage = other.gameObject.GetComponent<Bullet>().damage;
            enemyHealth -= damage;
            Destroy(other.gameObject);
        }
    }

    //If destroyed, explode into shiny things and give the plyer bullets
    private void OnDestroy()
    {
        playerController.playerBullets += enemyGiveBullets;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }

    void Fire()
    {
        //Ray ray = new Ray(enemyBulletSpawn.transform.position, player.transform.position); //draws a ray before shooting from the bullet spawner to the player
        //float distanceToPlayer = Vector3.Distance(enemyBulletSpawn.transform.position, player.transform.position);
        //Vector3 gunDownSights = ray.GetPoint(distanceToPlayer); 

        Vector3 randomFire = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * (1 - enemyAccuracy);
        enemyBulletDirection = player.transform.position + randomFire;

        if (enemyAttackTimer == enemyBulletFireRate)
        {
            //Instantiate bullet
            var bullet = Instantiate(enemyBulletPrefab, enemyBulletSpawn.position, enemyBulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = enemyBulletSpawn.transform.forward * enemyBulletSpeed;

            // Destroy the bullet after 2 seconds and reset attack timer
            Destroy(bullet, 2.0f);
            enemyAttackTimer = 0;
        }
    }

    //Pick a random vector around the player
    void RandomPosition()
    {
        int coinFlip = Random.Range(0, 2);
        if (coinFlip == 0)
        {
            randomDirection = new Vector3(Random.Range(-1f, -.5f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
        } else
        {
            randomDirection = new Vector3(Random.Range(.5f, 1f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
        }
        ray = new Ray(playerController.transform.position, randomDirection);
        //randomDir.transform.position = randomDirection;
        targetPosition = ray.GetPoint(Random.Range(4f, 7f));
    }
}
