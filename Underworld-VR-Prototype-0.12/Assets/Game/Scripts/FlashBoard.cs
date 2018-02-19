using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBoard : MonoBehaviour {

    private GameManager gameManager;
    private Renderer rend;

    public float alpha;

    public float enemiesDestroyedCounter;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        alpha -= Time.deltaTime * 2f;

        if (enemiesDestroyedCounter < gameManager.enemiesDestroyed)
        {
            alpha = 1;
            enemiesDestroyedCounter = gameManager.enemiesDestroyed;
        }

        rend.material.SetFloat("_Alpha", alpha);
		
	}
}
