using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int wave = 0;
    public string waveTimer;
    public float timeLeft;
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
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft <= 0 && waveActive)
        {
            timeLeft = 0;
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
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        float milliseconds = (timeLeft * 100f);
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
