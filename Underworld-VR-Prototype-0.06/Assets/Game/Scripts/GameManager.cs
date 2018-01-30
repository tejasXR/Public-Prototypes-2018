using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int roundCurrent = 0; // counter of the current round
    public string roundTimer;
    public float timeLeftCounter; //The variable that counts down in the update statement
    public float timeLeft; //The variable that shows how long each wave lasts
    public GameObject roundText;

    public TextMeshPro[] timeTextMeshPro;

    public bool roundActive; //To see if the player is currently in a round
    public bool upgradeActive; //To see if the player is currently upgrading
    public bool redemptionActive; //To see if the player is currently in redemption mode
    public bool gameOver;

    public float redemptionMeter;
    public float redemptionMeterMax;

    public bool hadRedemption; //Check if the player has gone through redemption in this play session


    // Use this for initialization
    void Start () {
        StartRound();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();

        if (roundActive)
        {
            timeLeftCounter -= Time.deltaTime;
        }

        if (redemptionActive)
        {
            redemptionMeter -= Time.deltaTime;
        }

        // If the counter has counted down to zero and the player is currently in a wave, stop the timer and enter upgrade mode
        if (timeLeftCounter <= 0 && roundActive && !redemptionActive)
        {
            timeLeftCounter = 0;
            upgradeActive = true;
            roundActive = false; //stop the wave after the waveTimer is over to put the player in upgrade mode
            //print("Upgrade!");
        }

        // If the player if done upgrading and the wave is already stopped, start the next wave
        if (!upgradeActive && !roundActive && !redemptionActive)
        {
            StartRound();
            roundActive = true;
        }

        // If the player has lost all health (called from Player script) and the round is active, stop round and enter redemption mode
        if (redemptionActive && roundActive && !hadRedemption)
        {
            StartRedemption();
            hadRedemption = false;
            roundActive = false;
        }

        if (redemptionMeter >= redemptionMeterMax)
        {
            redemptionActive = false;
            roundCurrent -= 1; // Reset the round number to when the player died
            StartRound(); // Start the wave
        } else if (redemptionMeter <= 0)
        {
            // If the player fails redemption, end the game
            GameOver();
        }

    }

    public void StartRound()
    {
        roundCurrent++;
        CheckRound();
        Instantiate(roundText);
        roundActive = true;
        redemptionActive = false;
        upgradeActive = false;
        timeLeftCounter = timeLeft;
    }

    void CheckRound()
    {
        switch (roundCurrent)
        {
            case 1:
                timeLeft = 60f; //Thirty seconds
                break;
            case 2:
                timeLeft = 60f; // A minute
                break;
            case 3:
                timeLeft = 60f * 1.5f; // A minute and a half
                break;
            case 4:
                timeLeft = 60f * 2f; //Two minutes
                break;
            case 5:
                timeLeft = 60f * 3f; //Three minutes
                break;
            case 6:
                timeLeft = 60f * 5f; //5 Minutes
                break;
        }
    }

    void UpdateTimer()
    {
        int minutes = Mathf.FloorToInt(timeLeftCounter / 60f);
        int seconds = Mathf.FloorToInt(timeLeftCounter % 60f);
        float milliseconds = (timeLeftCounter * 100f);
        milliseconds = milliseconds % 100;
        string niceTime = minutes.ToString("00") + " : " + seconds.ToString("00") + " : " + milliseconds.ToString("0");

        roundTimer = niceTime;

        foreach (TextMeshPro timeText in timeTextMeshPro)
        {
            timeText.text = roundTimer;
            //print(timeText);
        }
    }

    void StartRedemption()
    {
        redemptionMeter = 5;
    }

    void GameOver()
    {
        roundActive = false;
        redemptionActive = false;
        upgradeActive = false;
        gameOver = true;
    }
    
}
