using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public float enemySpawnTimer; //The time in seconds in which an enemy spawns
    public GameObject enemyPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0)
        {
            Instantiate(enemyPrefab);
            enemySpawnTimer = Random.Range(10f, 15f);
        }
		
	}
}
