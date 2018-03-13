using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager gameManager;

    //Define Basic Player Variables
    public Player playerController; //Whole of player object
    public GameObject player; //cameraEye object
    private Vector3 playerDirection; //Direction towards the player

    //Define Basic Enemy Variables
    public float enemyHealth;
    public float enemyGiveBullets; //Amount of bullets enemy gives to player after it is destroyed
    private Rigidbody rb;

    //Enemy Attack Behavior
    public float enemyBulletFireRate; //bullets fires per second
    public float enemyAttackTimer; //timer to know when to attack next
    public float enemyBulletSpeed; //speed of enemy bullet
    public Vector3 randomDirection; //random direction needed for enemy gun accuracy
    private Ray ray; //ray needed for enemy gun accuracy
    public float enemyAccuracy; //enemy accuray in % form
    public Vector3 enemyBulletDirection; //direction of the enemy bullet when generated

    public GameObject enemyBulletPrefab; //the enemy bullet gameObject
    public Transform enemyBulletSpawn; //the object for where to spawn the enemy bullet

    //Enemy Movement Behavior
    public Vector3 targetPosition; //the position that the enemy is constantly moving to
    public float enemyMoveTimer; //time in seconds before enemy switches places
    private Vector3 velocity = Vector3.zero; //velocity needed for smoothDamp movement

    //Enemy On Destroy
    public GameObject explosionPrefab; //the explosion effect when destroyed
    public GameObject earnBulletText; //the text object that tells players how many bullets they've earned
    private float enemyDestroyTimer = 2f;
    
    //public Text




    void Start()
    {
        //Define player variables for enemy prefab
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //Initiate RandomPosition to go to a random position when first created
        RandomPosition();

        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (gameManager.roundActive)
        {
            //Always move towards targetPosition if wave is active
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f, 5f);

            var direction = targetPosition - transform.position;
            var distance = Vector3.Distance(targetPosition, transform.position);

            //rb.MovePosition(Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f, 5f));
            //rb.MovePosition(targetPosition);

            //rb.AddForce(Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f, 5f));

            rb.AddForce(direction, ForceMode.Acceleration);

        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, -5, 0), ref velocity, 1f, 5f);
            DisappearAfterWave();
        }

        playerDirection = player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(playerDirection);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 30f * Time.deltaTime));

        
    }
    void Update()
    {
        if (gameManager.roundActive)
        {
            //Always move towards targetPosition if wave is active
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f, 5f);

           // var direction = transform.position - targetPosition;

            //rb.MovePosition(targetPosition * Time.deltaTime);
            //rb.MovePosition(targetPosition);
        } else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, -5, 0), ref velocity, 1f, 5f);
            DisappearAfterWave();
        }
        

        //Look at player
        //playerDirection = enemyBulletDirection - transform.position;
        //playerDirection = player.transform.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(playerDirection);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 20f * Time.deltaTime);


        if (gameManager.roundActive)
        {
            //If the attack timer equals the fire rate, then attack, else keep increasing the timer
            if (enemyAttackTimer < enemyBulletFireRate)
            {
                enemyAttackTimer += Time.deltaTime;
            }
            else
            {
                enemyAttackTimer = enemyBulletFireRate;
                Fire();
            }

            //Constantly decrease the moveTimer and if it hits zero, move
            enemyMoveTimer -= Time.deltaTime;
            if (enemyMoveTimer <= 0)
            {
                RandomPosition();
                enemyMoveTimer = Random.Range(5, 10);
            }
        }
    }

    //If player bullet hits enemy
    /*public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            var damage = other.gameObject.GetComponent<Bullet>().damage;
            enemyHealth -= damage;

            Vector3 otherVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            rb.AddForce(otherVelocity);
            Destroy(other.gameObject);

            if (enemyHealth <= 0)
            {
                EnemyDestroy();
                Destroy(this.gameObject);
            }
        }
    }*/

    // Trying onCollision
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            var damage = other.gameObject.GetComponent<Bullet>().damage;
            enemyHealth -= damage;

            Vector3 otherVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            rb.AddForce(otherVelocity/4);
            //Destroy(other.gameObject);

            if (enemyHealth <= 0)
            {
                EnemyDestroy();
                Destroy(this.gameObject);
            }
        }
    }


    void Fire()
    {
        
        Vector3 randomFire = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * (1 - enemyAccuracy);
        enemyBulletDirection = player.transform.position + randomFire;

        enemyBulletSpawn.LookAt(enemyBulletDirection);

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

        enemyBulletFireRate = Random.Range(.5f, 2f);
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

    //If destroyed by a bullet, explode into shiny things and give the player bullets
    void EnemyDestroy()
    {
        playerController.playerBullets += enemyGiveBullets;
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Instantiate(earnBulletText, transform.position, transform.rotation);
    }

    void DisappearAfterWave()
    {
        enemyDestroyTimer -= Time.deltaTime;
        if (enemyDestroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
