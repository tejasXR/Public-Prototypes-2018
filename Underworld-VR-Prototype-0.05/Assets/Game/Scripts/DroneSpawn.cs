using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawn : MonoBehaviour {

    public float enemySpawnTimer;
    private float spawnBufferTimer = 1.5f;
    public GameObject enemySpawnObj;

    private GameObject playerController;

    private bool hasSpawnedEnemy;


	// Use this for initialization
	void Start () {

        //enemySpawnObj.GetComponent<EnemyMovement>().RandomPosition();
        //print(enemySpawnObj.GetComponent<EnemyMovement>().targetPosition);

        playerController = GameObject.Find("PlayerController");
        //Vector3 looking = Vector3.Cross(transform.position - playerController.transform.position, Vector3.up);
        //Quaternion rotation = Quaternion.LookRotation(looking);
        //transform.rotation = Quaternion.LookRotation(looking, Vector3.up);
        transform.LookAt(playerController.transform.position);
	}
	
	// Update is called once per frame
	void Update () {

        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0 && !hasSpawnedEnemy)
        {
            Instantiate(enemySpawnObj, transform.position, enemySpawnObj.transform.rotation);
            hasSpawnedEnemy = true;
        }
        if (hasSpawnedEnemy)
        {
            spawnBufferTimer -= Time.deltaTime;
            if (spawnBufferTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }
        

    }
}
