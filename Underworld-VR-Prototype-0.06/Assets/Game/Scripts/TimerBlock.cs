using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBlock : MonoBehaviour {

    private GameManager gameManager;

    private float timeLeft;
    private float timeLeftCounter;

    private float scaleOriginal;
    private float scaleCurrent;

	// Use this for initialization
	void Start () {

        scaleOriginal = transform.localScale.x;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (timeLeft != gameManager.timeLeft)
        {
            timeLeft = gameManager.timeLeft;
        }

        timeLeftCounter = gameManager.timeLeftCounter;

        var timePercent = timeLeftCounter / timeLeft;

        scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal * timePercent, Time.deltaTime * 2f);

        transform.localScale= new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
		
	}
}
