using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int roundCurrent = 0; // counter of the current round
    public string roundTimer;

    public float timeLeftCounter; //The variable that counts down in the update statement
    public float timeLeft; //The variable that shows how long each wave lasts

    public float upgradeTimer;
    public float upgradeTimerCounter;

    public float redemptionMeter;
    public float redemptionMeterMax;

    //public GameObject roundText;
    public GameObject wallUI;

     public float roundFirstStartBufferTime; // The time between when the blue platforms are enabled and the purple stadium is enabled
     public float redemptionBufferTime; // Time between redemption mode starting and the meter counting down

    public TextMeshPro[] timeTextMeshPro;

    public bool gameStart;
    public bool playerMoveToStadium;
    public bool playerReachedStadium;
    public bool roundStart;
    public bool roundActive; //To see if the player is currently in a round
    public bool upgradeActive; //To see if the player is currently upgrading
    public bool redemptionActive; //To see if the player is currently in redemption mode
    public bool gameOver;

    public GameObject purpleStadium;
    public GameObject bluePlatform;
    public GameObject synthCity;

    public GameObject playerStartArea;
    public GameObject playerShield;


    public bool hadRedemption; //Check if the player has gone through redemption in this play session

    public PlatformScript playerPlatform;
    public GameStartUI gameStartUI;
    public WeaponActive weaponActive;

    private void Awake()
    {
        playerStartArea.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        CheckRound();

        redemptionBufferTime = 3f;
    }
	
	// Update is called once per frame
	void Update () {

        if (gameStartUI.gameStart && !playerMoveToStadium)
        {
            gameStart = true;
            playerPlatform.moving = true;
            playerMoveToStadium = true;
        }

        if (!playerPlatform.moving && playerMoveToStadium)
        {
            EnableStadium();
            playerPlatform.scaling = true;
            playerMoveToStadium = false;
            playerReachedStadium = true;

        }

        // Initialize before the first when, when the player has just reached the stadium
        if (playerReachedStadium)
        {
            roundFirstStartBufferTime -= Time.deltaTime;
            if (roundFirstStartBufferTime <= 0 && !roundStart)
            {
                roundActive = true;
                StartRound();
                playerShield.SetActive(true);
                weaponActive.WeaponToActivate("PISTOL");
                roundStart = true;
            }
        }

        if (roundActive)
        {
            UpdateTimer();
            timeLeftCounter -= Time.deltaTime;
        }

        if (upgradeActive)
        {
            upgradeTimerCounter -= Time.deltaTime;
            if (upgradeTimerCounter <= 0)
            {
                roundStart = true;
                upgradeActive = false;
                upgradeTimerCounter = upgradeTimer;
            }
        }

        if (redemptionActive)
        {
            redemptionBufferTime -= Time.deltaTime;
            if (redemptionBufferTime <= 0)
            {
                redemptionMeter -= Time.deltaTime;
            }
        }

        // If the counter has counted down to zero and the player is currently in a round, stop the timer and enter upgrade mode
        if (timeLeftCounter <= 0 && roundActive && !redemptionActive)
        {
            timeLeftCounter = 0;
            upgradeActive = true;
            roundStart = false;
            roundActive = false;  //stop the wave after the waveTimer is over to put the player in upgrade mode

            //print("Upgrade!");
        }

        // If the player if done upgrading and the wave is already stopped, start the next wave
        if (roundStart && !upgradeActive && !roundActive && !redemptionActive && roundCurrent > 0)
        {
            StartRound();
            roundActive = true;
            roundStart = false;
        }

        // If the player has lost all health (called from Player script) and the round is active, stop round and enter redemption mode
        if (redemptionActive && roundActive && !hadRedemption)
        {
            StartRedemption();
            hadRedemption = true;
            roundActive = false;
        }

        if (redemptionMeter >= redemptionMeterMax && redemptionActive)
        {
            roundCurrent -= 1; // Reset the round number to when the player died
            StartRound(); // Start the wave
            redemptionActive = false;
        }
        else if (redemptionMeter <= 0 && !gameOver && redemptionActive)
        {
            // If the player fails redemption, end the game
            GameOver();
        }
    }

    public void StartRound()
    {
        roundCurrent++;
        CheckRound();
        //Instantiate(roundText);
        wallUI.SetActive(true);
        roundActive = true;
        redemptionActive = false;
        upgradeActive = false;
        timeLeftCounter = timeLeft;
        //print("starting");
    }

    void CheckRound()
    {
        switch (roundCurrent)
        {
            case 0:
                timeLeft = 5f; //Thirty seconds
                break;
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
        redemptionMeter = 10;
        wallUI.SetActive(false);
    }

    void EnableStadium()
    {
        bluePlatform.SetActive(true);
        purpleStadium.SetActive(true);
        synthCity.SetActive(false);
        playerStartArea.SetActive(false);
    }

    void GameOver()
    {
        roundActive = false;
        redemptionActive = false;
        upgradeActive = false;
        gameOver = true;
    }
    
}
