using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private EnemyParent enemyParent;

    //Enemy Attack Behavior
    public float enemyBulletFireRate; //bullets fires per second
    private float enemyAttackTimer; //timer to know when to attack next
    public float enemyBulletSpeed; //speed of enemy bullet
    public float enemyBulletDamage;
    //public Vector3 randomDirection; //random direction needed for enemy gun accuracy
    //private Ray ray; //ray needed for enemy gun accuracy
    public float enemyBulletAccuracy; //enemy accuray in % form
    //public Vector3 enemyBulletDirection; //direction of the enemy bullet when generated

    public GameObject enemyBulletPrefab; //the enemy bullet gameObject
    public Transform[] enemyBulletSpawns; //the object for where to spawn the enemy bullet
    private int enemyBulletSpawnCounter;

    public bool isSingleDrone;
    public bool isDoubleDrone;

    // Use this for initialization
    void Start () {
        enemyParent = GetComponent<EnemyParent>();
        
    }

    // Update is called once per frame
    void Update () {
        if (enemyParent.gameManager.waveActive)
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
        }
    }

    void Fire()
    {
        float spreadFactor = 1 - enemyBulletAccuracy;

        var enemyBulletDirection = enemyParent.player.transform.position - transform.position;

        enemyBulletDirection.x += Random.Range(-spreadFactor, spreadFactor);
        enemyBulletDirection.y += Random.Range(-spreadFactor, spreadFactor);
        enemyBulletDirection.z += Random.Range(-spreadFactor, spreadFactor);

        enemyBulletDirection = enemyBulletDirection.normalized;

        //Vector3 randomFire = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * (1 - enemyAccuracy);
        //enemyBulletDirection = enemyParent.player.transform.position + randomFire;

        //enemyBulletSpawns[enemyBulletSpawnCounter].LookAt(enemyBulletDirection);

        if (enemyAttackTimer == enemyBulletFireRate)
        {
            //Instantiate bullet
            var bullet = Instantiate(enemyBulletPrefab, enemyBulletSpawns[enemyBulletSpawnCounter].position, Quaternion.LookRotation(enemyBulletDirection));

            // Add velocity to the bullet
            //bullet.GetComponent<Rigidbody>().velocity = enemyBulletSpawns[enemyBulletSpawnCounter].transform.forward * enemyBulletSpeed;
            bullet.GetComponent<Rigidbody>().velocity = enemyBulletDirection * enemyBulletSpeed;
            bullet.GetComponent<EnemyBullet>().enemyParent = this.gameObject;
            bullet.GetComponent<EnemyBullet>().damage = enemyBulletDamage;
            //bullet.transform.position
            //print(enemyBulletDirection);

            // Destroy the bullet after 2 seconds and reset attack timer
            Destroy(bullet, 2.0f);
            enemyAttackTimer = 0;
        }
        enemyBulletFireRate = Random.Range(.5f, 2f);

        enemyBulletSpawnCounter++;

        if (isSingleDrone)
        {
            if (enemyBulletSpawnCounter > 0) { enemyBulletSpawnCounter = 0; }
        } else if (isDoubleDrone)
        {
            if (enemyBulletSpawnCounter > 1) { enemyBulletSpawnCounter = 0; }
        }
    }

    
}
