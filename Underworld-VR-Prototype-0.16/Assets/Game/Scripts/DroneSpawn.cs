using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawn : MonoBehaviour {

    public float enemySpawnTimer;
    private float spawnBufferTimer = 1.5f;

    public float destroyTimer;
    //private float destroyTimerCounter;

    public GameObject enemySpawnObj;
    public Transform spawnPoint;
    private GameObject playerController;

    public GameObject mesh;
    private Renderer triangleRend;
    //public float fadeSpeed = 0f;
    public float fadeSpeed;
    private bool hasSpawnedEnemy;

    public Color mainColor;
    public Color glowColor;

    //public AudioSource spawnSound;
    //private float spawnSoundPitchOriginal;


    private void Awake()
    {
        triangleRend = mesh.GetComponent<Renderer>();

        //triangleAlpha = 1f;

        mainColor = triangleRend.material.GetColor("_Color");
        glowColor = triangleRend.material.GetColor("_MKGlowColor");

        //spawnSoundPitchOriginal = spawnSound.pitch;

    }


    // Use this for initialization
    void Start () {



        //enemySpawnObj.GetComponent<EnemyMovement>().RandomPosition();
        //print(enemySpawnObj.GetComponent<EnemyMovement>().targetPosition);

        playerController = GameObject.Find("PlayerController");
        //Vector3 looking = Vector3.Cross(transform.position - playerController.transform.position, Vector3.up);
        //Quaternion rotation = Quaternion.LookRotation(looking);
        //transform.rotation = Quaternion.LookRotation(looking, Vector3.up);
        //transform.LookAt(playerController.transform.position);




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

            destroyTimer -= Time.deltaTime;
            mainColor = Color.Lerp(mainColor, new Color(0, 0, 0, 0), Time.deltaTime * fadeSpeed);

            glowColor = Color.Lerp(glowColor, new Color(0, 0, 0, 0), Time.deltaTime * fadeSpeed);

            

            //triangleAlpha = Mathf.Lerp(triangleAlpha, 0f, Time.deltaTime * alphaSpeed);

            if (destroyTimer <= 0)
            {
                Destroy(this.gameObject);
            }


            //spawnSound.pitch = Mathf.Lerp(spawnSound.pitch, spawnSoundPitchOriginal * Time.timeScale, Time.deltaTime * 4f);

        }

        /*if (!hasSpawnedEnemy)
        {
            triangleAlpha = Mathf.Lerp(triangleAlpha, 0.4f, Time.deltaTime * alphaSpeed);
        } else
        {
            triangleAlpha = Mathf.Lerp(triangleAlpha, 0f, Time.deltaTime * alphaSpeed);
        }*/

        //triangleRend.material.SetFloat("_MKGlowPower", triangleAlpha);

        triangleRend.material.SetColor("_Color", mainColor);
        triangleRend.material.SetColor("_MKGlowColor", glowColor);
    }
}
