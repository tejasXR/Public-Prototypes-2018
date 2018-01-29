using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int wave = 0;
    public string waveTimer;
    public float timeLeftCounter; //The variable that counts down in the update statement
    public float timeLeft; //The variable that shows how long each wave lasts
    public GameObject waveText;

    public TextMeshPro[] timeTextMeshPro;

    public bool waveActive; //To see if the player is currently in a wave
    public bool upgradeActive; //To see if the player is currently upgrading


    // Use this for initialization
    void Start () {
        StartNewWave();
    }
	
	// Update is called once per frame
	void Update () {
        UpdateTimer();

        if (waveActive)
        {
            timeLeftCounter -= Time.deltaTime;
        }

        if (timeLeftCounter <= 0 && waveActive)
        {
            timeLeftCounter = 0;
            upgradeActive = true;
            waveActive = false; //stop the wave after the waveTimer is over to put the player in upgrade mode
            //print("Upgrade!");
        }

        // If the player if done upgrading and the wave is already stopped, start the next wave
        if (!upgradeActive && !waveActive)
        {
            StartNewWave();
            waveActive = true;
        }
    }

    public void StartNewWave()
    {
        wave++;
        CheckWave();
        Instantiate(waveText);
        waveActive = true;
        timeLeftCounter = timeLeft;
    }

    void CheckWave()
    {
        switch (wave)
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

        waveTimer = niceTime;

        foreach (TextMeshPro timeText in timeTextMeshPro)
        {
            timeText.text = waveTimer;
            //print(timeText);
        }
    }
    
}
