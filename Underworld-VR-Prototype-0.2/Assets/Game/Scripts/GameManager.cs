using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int wave;
    public string waveTimer;
    public float timeLeft;
    public GameObject waveText;

	// Use this for initialization
	void Start () {
        WaveText();
        CheckWave();
	}
	
	// Update is called once per frame
	void Update () {

        timeLeft -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        float milliseconds = (timeLeft * 1000f);
        milliseconds = milliseconds % 100;
        //string niceTime = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        string niceTime = minutes.ToString("00") + " : " + seconds.ToString("00") + " : " + milliseconds.ToString("0"); 

        waveTimer = niceTime;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            wave++;
            CheckWave();
            WaveText();
        }	
	}

    void CheckWave()
    {
        switch(wave)
        {
            case 1:
                timeLeft = 60f;
                break;
            case 2:
                timeLeft = 60f * 1.5f; // A minute and a half
                break;
            case 3:
                timeLeft = 60f * 2f; //Two minutes
                break;
            case 4:
                timeLeft = 60f * 3f; //Three minutes
                break;
            case 5:
                timeLeft = 60f * 5f; //5 Minutes
                break;
        }
    }

    void WaveText()
    {
        Instantiate(waveText);
    }
}
