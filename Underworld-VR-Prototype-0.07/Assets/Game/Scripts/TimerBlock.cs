using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBlock : MonoBehaviour {

    private GameManager gameManager;

    private float timeLeft;
    private float timeLeftCounter;

    private float scaleOriginal;
    private float scaleCurrent;

    public bool isRoundTimer;
    public bool isUpgradeTimer;
    public bool isRedemptionTimer;

    private Renderer rend;

	// Use this for initialization
	void Start () {

        scaleOriginal = transform.localScale.x;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rend = GetComponent<Renderer>();	
	}
	
	// Update is called once per frame
	void Update () {

        if (isRoundTimer)
        {
            if (timeLeft != gameManager.timeLeft)
            {
                timeLeft = gameManager.timeLeft;
            }

            timeLeftCounter = gameManager.timeLeftCounter;

            var timePercent = timeLeftCounter / timeLeft;

            scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal * timePercent, Time.deltaTime * 2f);

            transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
        }

        if (isUpgradeTimer)
        {
            if (timeLeft != gameManager.upgradeTimer)
            {
                timeLeft = gameManager.upgradeTimer;
            }

            timeLeftCounter = gameManager.upgradeTimerCounter;

            var timePercent = timeLeftCounter / timeLeft;

            scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal * timePercent, Time.deltaTime * 2f);

            transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
        }

        if (isRedemptionTimer)
        {
            if (timeLeft != gameManager.redemptionMeterMax)
            {
                timeLeft = gameManager.redemptionMeterMax;
            }

            timeLeftCounter = gameManager.redemptionMeter;

            var timePercent = timeLeftCounter / timeLeft;

            scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal * timePercent, Time.deltaTime * 2f);

            transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);

            rend.material.SetColor("_Color", Color.Lerp(Color.black, Color.red, timePercent + .2f));
        }
        
		
	}
}
