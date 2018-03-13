using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawn : MonoBehaviour {

    public float enemySpawnTimer;
    private float spawnBufferTimer = 1.5f;
    public GameObject enemySpawnObj;
    public Transform spawnPoint;
    private GameObject playerController;

    public GameObject mesh;
    private Renderer triangleRend;
    public float triangleAlpha = 0f;
    public float alphaSpeed;
    private bool hasSpawnedEnemy;

    private void Awake()
    {
        triangleAlpha = 1f;
    }


    // Use this for initialization
    void Start () {

        //enemySpawnObj.GetComponent<EnemyMovement>().RandomPosition();
        //print(enemySpawnObj.GetComponent<EnemyMovement>().targetPosition);

        playerController = GameObject.Find("PlayerController");
        //Vector3 looking = Vector3.Cross(transform.position - playerController.transform.position, Vector3.up);
        //Quaternion rotation = Quaternion.LookRotation(looking);
        //transform.rotation = Quaternion.LookRotation(looking, Vector3.up);
        transform.LookAt(playerController.transform.position);

        triangleRend = mesh.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        enemySpawnTimer -= Time.deltaTime;
        if (enemySpawnTimer <= 0 && !hasSpawnedEnemy)
        {
            Instantiate(enemySpawnObj, spawnPoint.transform.position, enemySpawnObj.transform.rotation);
            hasSpawnedEnemy = true;
        }
        if (hasSpawnedEnemy)
        {
            //spawnBufferTimer -= Time.deltaTime;

            //if (spawnBufferTimer <= 0)
            {
              //  Destroy(this.gameObject);
            }

            triangleAlpha = Mathf.Lerp(triangleAlpha, 0f, Time.deltaTime * alphaSpeed);

            if (triangleAlpha < .01)
            {
                Destroy(this.gameObject);
            }
            



        }

        /*if (!hasSpawnedEnemy)
        {
            triangleAlpha = Mathf.Lerp(triangleAlpha, 0.4f, Time.deltaTime * alphaSpeed);
        } else
        {
            triangleAlpha = Mathf.Lerp(triangleAlpha, 0f, Time.deltaTime * alphaSpeed);
        }*/

        triangleRend.material.SetFloat("_MKGlowPower", triangleAlpha);


    }
}
