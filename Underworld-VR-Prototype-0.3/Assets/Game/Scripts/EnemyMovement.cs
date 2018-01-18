using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    public EnemyParent enemyParent;

    //Enemy Movement Behavior
    public Vector3 targetPosition; //the position that the enemy is constantly moving to
    public float enemyMoveTimer; //time in seconds before enemy switches places
    private Vector3 velocity = Vector3.zero; //velocity needed for smoothDamp movement
    public float enemyMoveSpeed;
    private bool moveNow;
    //public float enemyMaxSpeed;

    // Ability to define movemt based on drone type
    //public bool enemySingleDrone;
    //public bool enemyDoubleDrone;
    public bool isBomberDrone;

    public float enemyMoveFrequencyMin;
    public float enemyMoveFrequencyMax;

    public float bomberRandChance; //The random chance that the bomber drone stops moving to random locations, and moves towards the player for the explosion
    public bool bomberMove;
    public float bomberBufferTimer = 5f; // A slight buffer so that the bomber does not move towards the player right away

    private Rigidbody rb;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        enemyParent = GetComponent<EnemyParent>();
	}
	
	// Update is called once per frame
	void Update () {

        enemyMoveTimer -= Time.deltaTime;
        
        if (bomberBufferTimer <= 0)
        {
            bomberBufferTimer = 0;
        } else
        {
            bomberBufferTimer -= Time.deltaTime;
        }

        if (enemyMoveTimer <= 0)
        {
            moveNow = false;
            
            if (isBomberDrone && !bomberMove && bomberBufferTimer <= 0)
            {
                //print("possibility of bomb");
                bomberRandChance = Random.Range(0, 2);
                if (bomberRandChance == 1)
                {
                    bomberMove = true;
                    MoveToPlayer();
                }
            }
            if (!bomberMove)
            {
                RandomPosition();
            }

            enemyMoveTimer = Random.Range(enemyMoveFrequencyMin, enemyMoveFrequencyMax);

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Vector3 otherVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            rb.AddForce(otherVelocity / 5);
        }

        
        if (other.gameObject.tag == "HitBody" && isBomberDrone)
        {
            enemyParent.BomberDestroy();
        }
    }

    void FixedUpdate()
    {
        if (enemyParent.gameManager.waveActive)
        {
            //Always move towards targetPosition if wave is active
            var direction = targetPosition - transform.position;
            //var distance = Vector3.Distance(targetPosition, transform.position);

            rb.AddForce(direction * enemyMoveSpeed, ForceMode.Force);
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, -5, 0), ref velocity, 1.5f, 2f);
            enemyParent.DisappearAfterWave();
        }

        enemyParent.playerDirection = enemyParent.player.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(enemyParent.playerDirection);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 30f * Time.deltaTime));
    }

    void RandomPosition()
    {
        if (!moveNow)
        {
            Vector3 randomPosition;

            int coinFlip = Random.Range(0, 2);
            if (coinFlip == 0)
            {
                randomPosition = new Vector3(Random.Range(-1f, -.5f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
            }
            else
            {
                randomPosition = new Vector3(Random.Range(.5f, 1f), Random.Range(0f, .75f), Random.Range(-1f, 1f));
            }

            Ray ray = new Ray(enemyParent.playerController.transform.position, randomPosition);
            targetPosition = ray.GetPoint(Random.Range(4f, 7f));
        }

        int layerMask = 1 << 8;

        // Makes sure we don't collide into the player
        if (!Physics.Raycast (transform.position, (targetPosition - transform.position), Mathf.Infinity, layerMask))
        {
            moveNow = true;
        }

    }

    void MoveToPlayer()
    {
        targetPosition = enemyParent.playerController.transform.position;
        enemyMoveSpeed = enemyMoveSpeed / 2;
    }


}
