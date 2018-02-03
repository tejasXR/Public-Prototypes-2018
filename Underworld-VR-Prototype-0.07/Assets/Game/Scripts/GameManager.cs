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
    public GameObject redemptionUI;

     public float roundFirstStartBufferTime; // The time between when the blue platforms are enabled and the purple stadium is enabled
     public float redemptionBufferTime; // Time between redemption mode starting and the meter counting down

    public TextMeshPro[] timeTextMeshPro;

    public bool gameStart;
    public bool playerMoveToStadium;
    public bool playerReachedStadium;
    public bool mainGameStart;
    public bool roundStart;
    public bool roundActive; //To see if the player is currently in a round
    public bool upgradeActive; //To see if the player is currently upgrading

    public bool redemptionStart;
    public bool redemptionPreStart;
    public bool redemptionActive; //To see if the player is currently in redemption mode
    public bool hadRedemption; //Check if the player has gone through redemption in this play session

    public bool gameOver;

    public GameObject purpleStadium;
    public GameObject bluePlatform;
    public GameObject synthCity;

    public GameObject playerStartArea;
    public GameObject playerShield;

    public GameObject platformLight;
    public GameObject redemptionLight;

    public GameObject[] controllerModels;


    public PlatformScript playerPlatform;
    public GameStartUI gameStartUI;
    public WeaponActive weaponActive;
    public Player playerController;

    private void Awake()
    {
        playerStartArea.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        CheckRound();
    }
	
	// Update is called once per frame
	void Update () {

        if (gameStartUI.gameStart && !playerMoveToStadium && !playerReachedStadium)
        {
            gameStart = true;
            playerPlatform.moving = true;
            playerMoveToStadium = true;
            foreach (GameObject controllerModel in controllerModels)
            {
                controllerModel.SetActive(false);
            }

        }

        if (!playerPlatform.moving && playerMoveToStadium)
        {
            EnableStadium();
            playerPlatform.scaling = true;
            playerMoveToStadium = false;
            playerReachedStadium = true;

        }

        // Initialize before the first when, when the player has just reached the stadium
        if (playerReachedStadium && !mainGameStart)
        {
            roundFirstStartBufferTime -= Time.deltaTime;
            if (roundFirstStartBufferTime <= 0 && !roundStart)
            {
                //roundActive = true;
                //StartRound();
                playerShield.SetActive(true);
                weaponActive.WeaponToActivate("PISTOL");
                roundStart = true;
                mainGameStart = true;
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

        if (redemptionStart && redemptionBufferTime > 0)
        {
            redemptionBufferTime -= Time.deltaTime;
            if (redemptionBufferTime < 3 && !redemptionPreStart)
            {
                PreRedemption();
                redemptionPreStart = true;
            }
            if (redemptionBufferTime <= 0)
            {
                redemptionActive = true;
                StartRedemption();
                redemptionStart = false;
            }
        }

        if (redemptionActive)
        {
            redemptionMeter -= Time.deltaTime;
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
        if (roundStart && !upgradeActive && !roundActive && !redemptionActive)
        {
            StartRound();
            roundActive = true;
            roundStart = false;
        }

        // If the player has lost all health (called from Player script) and the round is active, stop round and enter redemption mode
        if (playerController.playerHealth <= 0 && !hadRedemption)
        {
            redemptionStart = true;
            print("redemption start");
        }

        if (redemptionStart && roundActive)
        {
            TurnOffForRedemption(); // Turn stuff off for redemption
            //redemptionActive = true; // Not false here, will call when redemption buffer timer runs down
            hadRedemption = true;
            roundActive = false;
            //redemptionStart = false; // Not false here becuase we need the buffer time to run out first
        }

        if (redemptionMeter >= redemptionMeterMax && redemptionActive)
        {
            roundCurrent -= 1; // Reset the round number to when the player died
            StopRedemption();
           
            //redemptionMeter
            playerController.playerHealth += playerController.playerHealthMax / 2;
            roundStart = true;
            redemptionActive = false;
            //StartRound(); // Start the wave

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
        
        
        
       
        timeLeftCounter = timeLeft;
        
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
        redemptionUI.SetActive(true);
        playerShield.SetActive(false);
    }

    // Turn on light and give player sword for pre-redemption timing
    void PreRedemption()
    {
        weaponActive.WeaponToActivate("SABER SWORD");
        redemptionLight.SetActive(true);
    }

    // Turn stuff off (black-out) for the redemption buffer timer to run down
    void TurnOffForRedemption()
    {
        wallUI.SetActive(false);
        purpleStadium.SetActive(false);
        bluePlatform.SetActive(false);
        platformLight.SetActive(false);
        playerShield.SetActive(false);
        weaponActive.DisableAllWeapons();
    }

    void StopRedemption()
    {
        redemptionUI.SetActive(false);
        purpleStadium.SetActive(true);
        bluePlatform.SetActive(true);
        platformLight.SetActive(true);
        redemptionLight.SetActive(false);
        weaponActive.WeaponToActivate(weaponActive.previousWeapon);
        playerShield.SetActive(true);
    }

    void EnableStadium()
    {
        bluePlatform.SetActive(true);
        purpleStadium.SetActive(true);
        synthCity.SetActive(false);
        playerStartArea.SetActive(false);
        platformLight.SetActive(true);
    }

    void GameOver()
    {
        roundActive = false;
        redemptionActive = false;
        upgradeActive = false;
        gameOver = true;
    }
    
}
