using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveTimer : MonoBehaviour {

    public GameManager gameManager;
    private TextMeshPro timerText;

	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        timerText = GetComponent<TextMeshPro>();
	}
	
	// Update is called once per frame
	void Update () {
        timerText.text = gameManager.roundTimer;
	}
}
