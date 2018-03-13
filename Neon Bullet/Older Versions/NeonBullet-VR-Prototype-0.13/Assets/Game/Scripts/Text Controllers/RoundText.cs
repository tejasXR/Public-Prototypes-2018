using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundText : MonoBehaviour {

    public GameManager gameManager;
    private TextMeshPro roundText;

	// Use this for initialization
	void Start () {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        roundText = GetComponent<TextMeshPro>();

        CheckRound();

	}
	
	// Update is called once per frame
	void Update () {
		

	}

    void CheckRound()
    {
        switch(gameManager.roundCurrent)
        {
            case 1:
                roundText.text = "Round \n " +
                                    "One";
                break;
            case 2:
                roundText.text = "Round \n " +
                                    "Two";
                break;
            case 3:
                roundText.text = "Round \n " +
                                    "Three";
                break;
            case 4:
                roundText.text = "Round \n " +
                                    "Four";
                break;
        }
    }

}
