using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesInGameCount : MonoBehaviour {

    private GameManager gameManager;
    private TextMeshPro text;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        text = GetComponent<TextMeshPro>();
	}
	
	// Update is called once per frame
	void Update () {

        text.text = "enemies on screen: " + gameManager.enemiesOnScreen;
		
	}
}
