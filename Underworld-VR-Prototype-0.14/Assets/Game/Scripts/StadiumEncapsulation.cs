using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumEncapsulation : MonoBehaviour {

    private Renderer rend;
    public Color[] colors;
    public Color colorCurrent;
    private GameManager gameManager;
    public float enemiesDestroyedCounter;


    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rend = GetComponent<Renderer>();
		
	}

    private void OnEnable()
    {
        //colorCurrent = Color.black;
    }

    // Update is called once per frame
    void Update () {

        //if (gameManager.inRound)
        {
        }

        /*if (gameManager.inRedemption)
        {
            colorCurrent = Color.Lerp(colorCurrent, colors[1], Time.deltaTime);
        }*/

        if (gameManager.enemiesToSpawn > 0)
        {
            if (enemiesDestroyedCounter != gameManager.enemiesDestroyed)
            {
                colorCurrent = colors[1];
                enemiesDestroyedCounter = gameManager.enemiesDestroyed;

            }
        }

        if (gameManager.inRound)
        {
            colorCurrent = Color.Lerp(colorCurrent, colors[0], Time.deltaTime);
            rend.material.SetColor("_MKGlowTexColor", Color.Lerp(rend.material.GetColor("_MKGlowTexColor"), colorCurrent, Time.deltaTime));
        }        
    }
}
