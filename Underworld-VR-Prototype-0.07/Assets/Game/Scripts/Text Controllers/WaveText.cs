using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveText : MonoBehaviour {

    public GameManager gameManager;
    private TextMeshPro waveText;

	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveText = GetComponent<TextMeshPro>();

        CheckWave();

	}
	
	// Update is called once per frame
	void Update () {
		

	}

    void CheckWave()
    {
        switch(gameManager.roundCurrent)
        {
            case 1:
                waveText.text = "Wave \n " +
                                    "One";
                break;
            case 2:
                waveText.text = "Wave \n " +
                                    "Two";
                break;
            case 3:
                waveText.text = "Wave \n " +
                                    "Three";
                break;
            case 4:
                waveText.text = "Wave \n " +
                                    "Four";
                break;
        }
    }

}
