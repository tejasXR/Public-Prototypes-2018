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
    public bool canFire;

    public Vector3 lookDirection;
    private float lookAtMovePosTimer; // The timer before the enemy stops looking at the move position and looks at the player
    private float attackBufferTimer = 1.5f;
    //public float enemyMaxSpeed;

    // Ability to define movemt based on drone type
    //public bool enemySingleDrone;
    //public bool enemyDoubleDrone;
    public bool alwaysFacingPlayer;

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

        
        rb.AddForce(Vector3.forward * enemyMoveSpeed, ForceMode.Acceleration);
	}
	
	// Update is called once per frame
	void Update () {

        enemyMoveTimer -= Time.deltaTime;
        lookAtMovePosTimer -= Time.deltaTime;
        
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

        if (alwaysFacingPlayer)
        {
            canFire = true;
            lookDirection = enemyParent.player.transform.position;
        } else
        {
            if (lookAtMovePosTimer <= 0)
            {
                lookDirection = enemyParent.player.transform.position;
                attackBufferTimer -= Time.deltaTime;

                if (attackBufferTimer <= 0)
                {
                    canFire = true;
                }

            }
            else
            {
                lookDirection = targetPosition;
                canFire = false;
            }
        }

        


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "DeflectedBullet")
        {
            //Vector3 otherVelocity = other.gameObject.GetComponent<Rigidbody>().velocity;
            //rb.AddForce(otherVelocity.normalized / 4);
        }
    }

    void FixedUpdate()
    {
        if (enemyParent.gameManager.roundActive && enemyParent.enemyHealth > 0)
        {
            //Always move towards targetPosition if wave is active
            var direction = targetPosition - transform.position;
            //var distance = Vector3.Distance(targetPosition, transform.position);          

            rb.AddForce(direction * enemyMoveSpeed, ForceMode.Acceleration);
        }
        else if (!enemyParent.gameManager.roundActive || enemyParent.gameManager.redemptionActive)
        {
            transform.position = Vector3.SmoothDamp(transform.position, new Vector3(0, -5, 0), ref velocity, 1.5f, 5f);
            enemyParent.DisappearAfterWave();
        }

        // Controls max speed of enemy;
        /*if (rb.velocity.magnitude > (enemyMoveSpeed*4))
        {
            rb.velocity = rb.velocity.normalized * (enemyMoveSpeed * 4);
        }*/



        //enemyParent.playerDirection = enemyParent.player.transform.position - transform.position;


        //lookDirection = enemyParent.player.transform.position;
        //lookDirection = lookPosition;


        if (!isBomberDrone)
        {
            Vector3 looking = lookDirection - transform.position;

            Quaternion rotation = Quaternion.LookRotation(looking);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, rotation, 3f * Time.deltaTime));
        }

        //Quaternion rotation = Quaternion.LookRotation(enemyParent.playerDirection);

        // Create a variable to enable dyanmic rotation towrds different Vectors (i.e., th  player, move position)

        //Quaternion rotation = Quaternion.LookRotation(lookDirection);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);


        if (isBomberDrone && bomberMove)
        {
            if (Vector3.Distance(transform.position, enemyParent.player.transform.position) < 1f)
            {
                enemyParent.BomberDestroy();
            }
        }

    }

    public void RandomPosition()
    {
        lookAtMovePosTimer = 2.5f;
        attackBufferTimer = 2f;

        if (!moveNow && enemyParent.enemyHealth > 0)
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

            Ray ray = new Ray(enemyParent.player.transform.position, randomPosition);
            targetPosition = ray.GetPoint(Random.Range(3f, 6f));
        }

        int layerMask = 1 << 8;
        RaycastHit hit;
        // Makes sure we don't collide into the player
        if (!Physics.SphereCast(transform.position, 2f, (targetPosition - transform.position), out hit, Vector3.Distance(transform.position, targetPosition), layerMask))
        {
            moveNow = true;
        }

       /*
        if (!Physics.Raycast (transform.position, (targetPosition - transform.position), Mathf.Infinity, layerMask))
        {
            // if there are no enemies with a 1 unit radius
            if (!Physics.CheckSphere(targetPosition, 1f))
            {
                moveNow = true;
            }
        }*/
    }

    void MoveToPlayer()
    {
        targetPosition = enemyParent.player.transform.position;
        enemyMoveSpeed = enemyMoveSpeed / 4;
    }
}
