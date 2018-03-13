using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillBoards : MonoBehaviour {

    private GameManager gameManager;
    public GameObject solidFill;
    public GameObject solidFillMesh;
    public float enemiesDestroyedPercent;
    public Vector3 scaleOriginal;
    public Vector3 scaleCurrent;

    public TextMeshPro percentText;

    public Color colorCurrent;
    public Color colorOriginal;

    private Renderer rend;

    public bool flash;

    public float enemiesDestroyedCounter;

    public StadiumFlicker outlineFlicker;




    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scaleOriginal = solidFill.transform.localScale;
        scaleCurrent = Vector3.zero;


        rend = solidFillMesh.GetComponent<Renderer>();

        colorOriginal = rend.material.GetColor("_Color");




    }

    private void OnEnable()
    {
        outlineFlicker.shouldFlicker = true;
    }

    // Update is called once per frame
    void Update () {


        if (gameManager.roundActive)
        {
            //print(gameManager.enemiesDestroyed / gameManager.enemiesToSpawn);
            enemiesDestroyedPercent = Mathf.Lerp(enemiesDestroyedPercent, (gameManager.enemiesDestroyed / gameManager.enemiesToSpawn), Time.deltaTime * 3.5f);

            scaleCurrent = Vector3.Lerp(scaleCurrent, scaleOriginal * enemiesDestroyedPercent, Time.deltaTime * 3.5f);

            percentText.text = Mathf.RoundToInt(enemiesDestroyedPercent * 100).ToString() + "%";

            solidFill.transform.localScale = scaleCurrent;

            if (enemiesDestroyedCounter < gameManager.enemiesDestroyed)
            {
                colorCurrent = Color.white;
                //outlineFlicker.shouldFlicker = true;
                enemiesDestroyedCounter = gameManager.enemiesDestroyed;
            }

            if (Mathf.Abs(enemiesDestroyedPercent - (gameManager.enemiesDestroyed / gameManager.enemiesToSpawn)) < .01f)
            {
                //outlineFlicker.shouldFlicker = false;
            } else
            {
                outlineFlicker.shouldFlicker = true;
            }

            colorCurrent = Color.Lerp(colorCurrent, colorOriginal, Time.deltaTime * 1.5f);

        }

        rend.material.SetColor("_Color", colorCurrent);

        

		
	}
}
