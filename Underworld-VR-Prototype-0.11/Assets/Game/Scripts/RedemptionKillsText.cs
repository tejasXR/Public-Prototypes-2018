using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RedemptionKillsText : MonoBehaviour {

    public TextMeshPro killNumText;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        killNumText.text = gameManager.enemiesDestroyed.ToString();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
