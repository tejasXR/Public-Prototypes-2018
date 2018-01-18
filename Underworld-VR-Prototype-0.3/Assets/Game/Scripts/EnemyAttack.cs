using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    private EnemyParent enemyParent;

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
        Vector3 randomFire = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)) * (1 - enemyAccuracy);
        enemyBulletDirection = enemyParent.player.transform.position + randomFire;

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
}
