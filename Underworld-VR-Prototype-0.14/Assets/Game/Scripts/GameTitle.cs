using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTitle : MonoBehaviour {

    private GameManager gameManager;

    public float delay;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
