using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemySpawnBoards : MonoBehaviour {

    public List<TriangleOutline> triangleOutlines = new List<TriangleOutline>();

    private GameManager gameManager;

    public float flickerFrequency; // The frequency of the flickering;
    public float flickerTimer;
    public float flickerDuration; //How long each traingle in the stadium shall remain disabled
    public float flickerDurationTimer;
    //public GameObject[] triangleStadiums;
    public bool flickerActive;
    public int triangleOutlineCount;

    public bool shouldFlicker;

    //public GameObject solidFill;
    //public GameObject solidFillMesh;
    //public float enemiesDestroyedPercent;
    //public Vector3 scaleOriginal;
    //public Vector3 scaleCurrent;

    //public TextMeshPro percentText;

    //public Color colorCurrent;
    public Color[] normalColors;
    public Color[] lightDroneSpawnColors;
    public Color[] fastDroneSpawnColors;
    public Color[] heavyDroneSpawnCoors;
    public Color[] bomberDroneSpawnColors;

    public int enemySpawnType;

    public GameObject[] enemySpawns;
    public Transform enemySpawnPoint;

    public AudioSource countDownSound;
        // 0 = light drone
        // 1 = fast drone
        // 2 = heavy drone
        // 3 = bomber drone

    //public bool isLightDroneSpawn;
    //public bool isFastDroneSpawn;
    //public bool isHeavyDroneSpawn;
    //public bool isBomberDoneSpawn;

    //public Color colorOriginal;

    //private Renderer rend;

    //public bool flash;

    //public float enemiesDestroyedCounter;

    //public StadiumFlicker outlineFlicker;




    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        flickerDurationTimer = flickerDuration;
        //scaleOriginal = solidFill.transform.localScale;
        //scaleCurrent = Vector3.zero;


        //rend = solidFillMesh.GetComponent<Renderer>();

        //colorOriginal = rend.material.GetColor("_Color");




    }

    private void OnEnable()
    {
        //outlineFlicker.shouldFlicker = true;
    }

    // Update is called once per frame
    void Update () {
        //countDownSound.volume = Mathf.Lerp(countDownSound.volume, 1, Time.deltaTime * .5f);
        //countDownSound.pitch = Mathf.Lerp(countDownSound.pitch, .5f, Time.deltaTime * .5f);

        FlickerDown();
        //if (gameManager.roundActive)
        {
            //print(gameManager.enemiesDestroyed / gameManager.enemiesToSpawn);
            if (gameManager.enemiesToSpawn > 0)
            {
                //enemiesDestroyedPercent = Mathf.Lerp(enemiesDestroyedPercent, (gameManager.enemiesDestroyed / gameManager.enemiesToSpawn), Time.deltaTime * 3.5f);

            }

            //scaleCurrent = Vector3.Lerp(scaleCurrent, scaleOriginal * enemiesDestroyedPercent, Time.deltaTime * 3.5f);

            //percentText.text = Mathf.RoundToInt(enemiesDestroyedPercent * 100).ToString() + "%";

            //solidFill.transform.localScale = scaleCurrent;

            //if (enemiesDestroyedCounter != gameManager.enemiesDestroyed)
            {
                //rend.material.SetColor("_Color", Color.white);
                //enemiesDestroyedCounter = gameManager.enemiesDestroyed;
                //outlineFlicker.shouldFlicker = true;


            }

            /*if (Mathf.Abs(enemiesDestroyedPercent - (gameManager.enemiesDestroyed / gameManager.enemiesToSpawn)) > .01f)
            {
                outlineFlicker.shouldFlicker = true;
            }
            */
            //colorCurrent = Color.Lerp(colorCurrent, colorOriginal, Time.deltaTime * 1.5f);

        }

        //rend.material.SetColor("_Color", Color.Lerp(rend.material.GetColor("_Color"), colorOriginal, Time.deltaTime));
        //print(rend.material.GetColor("_Color"));



        for (int i = 0; i < triangleOutlines.Capacity; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Renderer rend = triangleOutlines[i].lines[j].GetComponent<Renderer>();
                rend.material.SetColor("_Color", Color.Lerp(rend.material.GetColor("_Color"), normalColors[i], Time.deltaTime / 2f));
                rend.material.SetColor("_MKGlowColor", Color.Lerp(rend.material.GetColor("_MKGlowColor"), normalColors[i], Time.deltaTime / 2f));

            }

        }
	}

    void FlickerDown() 
    {
        if (shouldFlicker)  // linked to enemy Spawn manager
        {
            flickerTimer -= Time.deltaTime;
            if (flickerTimer <= 0)// && !flickerActive)
            {
                //flickerActive = true;
                flickerTimer = 0;
                flickerDurationTimer -= Time.deltaTime;
                if (flickerDurationTimer > 0)
                {
                    triangleOutlines[triangleOutlineCount].outline.SetActive(false);
                }
                else
                {
                    triangleOutlines[triangleOutlineCount].outline.SetActive(true);

                    for(int i = 0; i < 3; i++)
                    {
                        Renderer rend = triangleOutlines[triangleOutlineCount].lines[i].GetComponent<Renderer>();
                        switch (enemySpawnType)
                        {
                            case 0:
                                //print("light drone colors");
                                rend.material.SetColor("_Color", lightDroneSpawnColors[triangleOutlineCount]);
                                rend.material.SetColor("_MKGlowColor", lightDroneSpawnColors[triangleOutlineCount]);
                                break;
                            case 1:
                                rend.material.SetColor("_Color", fastDroneSpawnColors[triangleOutlineCount]);
                                rend.material.SetColor("_MKGlowColor", fastDroneSpawnColors[triangleOutlineCount]);
                                break;
                            case 2:
                                rend.material.SetColor("_Color", heavyDroneSpawnCoors[triangleOutlineCount]);
                                rend.material.SetColor("_MKGlowColor", heavyDroneSpawnCoors[triangleOutlineCount]);
                                break;
                            case 3:
                                rend.material.SetColor("_Color", bomberDroneSpawnColors[triangleOutlineCount]);
                                rend.material.SetColor("_MKGlowColor", bomberDroneSpawnColors[triangleOutlineCount]);
                                break;
                        }
                    }

                    triangleOutlineCount++;

                    if (triangleOutlineCount == (triangleOutlines.Capacity))
                    {
                        shouldFlicker = false;
                        flickerTimer = flickerFrequency;
                        triangleOutlineCount = 0;
                        InstantiateSpawner();
                    }
                    else
                    {
                        //flickerActive = false;
                        flickerDurationTimer = flickerDuration;
                    }
                }
            }
        }
    }

    void InstantiateSpawner()
    {
        Instantiate(enemySpawns[enemySpawnType], enemySpawnPoint.position, enemySpawnPoint.rotation);
    } 


    [System.Serializable]
    public class TriangleOutline
    {
        public GameObject outline;
        public GameObject[] lines;
    }
}
