using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int roundCurrent = 0; // counter of the current round
    public string roundTimer;

    public float timeLeftCounter; //The variable that counts down in the update statement
    public float timeLeft; //The variable that shows how long each wave lasts

    public float enemiesToSpawn; //Keeps track of how many total enemies to Spawn in a round
    public float enemiesDestroyed; //Keeps track of how many enemies the player has destroyed
    //public int enemiesLeft;
    public float enemiesOnScreen; //Keeps track of how many enemies are currently on screen
    public float enemiesOnScreenMax;

    //public int redemptionEnemiestoSpawn;

    //public bool upgradeRound;
    //public int roundUntilNextUpgrade; // a bool that tells is the player will upgrade 
    public float upgradeTimer;
    public float upgradeTimerCounter;

    public float redemptionMeter; //Was a timer before, but now a chain-like meeter
    public float redemptionMeterMax;

    public GameObject roundText;
    public GameObject roundCompleteUI;

    public GameObject roundUI;
    public GameObject redemptionUI;

    public GameObject gameOverUI;

    public float roundFirstStartBufferTime; // The time between when the blue platforms are enabled and the purple stadium is enabled
    public float roundBufferTime;
    private float roundBufferTimeCounter; // The float that counts down between rounds
    public float redemptionBufferTime; // Time between redemption mode starting and the meter counting down

    public TextMeshPro[] timeTextMeshPro;

    public bool gameStart;
    public bool playerMoveToStadium;
    public bool playerReachedStadium;
    public bool mainGameStart;
    public bool roundStart;
    public bool roundActive; //To see if the player is currently in a round
    public bool inRound; // To see if the player is in any part of the round mode

    public bool upgradeActive; //To see if the player is currently upgrading

    public bool redemptionStart;
    public bool redemptionPreStart;
    public bool redemptionActive; //To see if the player is currently in redemption mode with enemies
    public bool inRedemption; // To see if the player is in any part of redemption mode;
    public bool hadRedemption; //Check if the player has gone through redemption in this play session

    public bool gameOver;

    public GameObject purpleStadium;
    public GameObject bluePlatform;
    public GameObject synthCity;
    public GameObject redemptionPlatform;
    public GameObject encapsulatingStadium;
    //public GameObject platformPieces;
    public GameObject gameTitle;

    public GameObject playerStartArea;
    public GameObject playerShield;

    public GameObject platformLight;
    public GameObject redemptionLight;

    public GameObject[] controllerModels;


    public PlatformScript playerPlatform;
    public GameStartUI gameStartUI;
    public WeaponActive weaponActive;
    public Player playerController;
    public MusicManager musicManager;
    public UpgradeManager upgradeManager;
    public TutorialManager tutorialManager;

    private void Awake()
    {
        playerStartArea.SetActive(true);
    }

    // Use this for initialization
    void Start () {
        CheckRound();
        upgradeTimerCounter = upgradeTimer;
        roundBufferTimeCounter = roundBufferTime;
        GameReset(); // sets all controlled objects active / inactive at the very start of the game
    }

    // Update is called once per frame
    void Update () {

        if (gameStartUI.gameStart && !playerMoveToStadium && !playerReachedStadium)
        {
            gameStart = true;
            playerPlatform.moving = true;
            playerMoveToStadium = true;
            //musicManager.musicVolume = 1f;
            tutorialManager.TutorialReset();
            foreach (GameObject controllerModel in controllerModels)
            {
                controllerModel.SetActive(false);
            }

            //playerController.playerBullets = 25;

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
                inRound = true;
                mainGameStart = true;
            }
        }

        if (roundStart && !upgradeActive && !roundActive && !redemptionActive)
        {
            StartRound();
            roundActive = true;
            roundStart = false;
        }

        if (playerController.playerHealth <= 0 && inRound)
        {
            gameOver = true;
            //print("redemption start");
        }

        if (gameOver)
        {
            inRound = false;
            roundActive = false;
            //redemptionActive = false;
            //upgradeActive = false;
            //gameOver = true;
            GameOver();
        }

        if (roundActive)
        {
            //UpdateTimer();
            //timeLeftCounter -= Time.deltaTime;

            if (enemiesDestroyed == enemiesToSpawn && enemiesOnScreen <= 0)// && !redemptionActive)
            {
                roundBufferTimeCounter -= Time.deltaTime;
                if (roundBufferTimeCounter <= 0)
                {
                    inRound = true;
                    inRedemption = false;
                    roundStart = true;
                    roundActive = false;
                    roundBufferTimeCounter = roundBufferTime;
                }
                //if (upgradeRound)
                {
                    //upgradeActive = true;
                    //roundStart = false;
                    //roundActive = false;  //stop the wave after the waveTimer is over to put the player in upgrade mode
                } //else
                {
                    //  roundStart = true;
                    //   roundActive = false;
                }
                //timeLeftCounter = 0;
               
                //print("Upgrade!");
            }

            

            /*if (redemptionStart)
            {
                TurnOffForRedemption(); // Turn stuff off for redemption
                                        //redemptionActive = true; // Not false here, will call when redemption buffer timer runs down
                hadRedemption = true;
                inRedemption = true;
                inRound = false;
                roundActive = false;
                //redemptionStart = false; // Not false here becuase we need the buffer time to run out first
            }

        }

        if (upgradeActive)
        {
            if (!upgradeManager.upgradeDone && upgradeManager.upgradeStart)
            {
                upgradeTimerCounter -= Time.deltaTime;
            }

            if (upgradeManager.upgradeDone || upgradeTimerCounter <= 0)
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

            if (enemiesDestroyed >= 3) // If the play has destroyed all the enemies
            {
                roundCurrent -= 1; // Reset the round number to when the player died
                StopRedemption();
                //redemptionMeter
                playerController.playerHealth += (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier) / 2;
                roundStart = true;
                redemptionActive = false;
                redemptionPreStart = false;
                //StartRound(); // Start the wave

            }
            else if (redemptionMeter <= 0 && !gameOver)
            {
                // If the player fails redemption, end the game
                GameOver();
            }*/
        }

        // If the counter has counted down to zero and the player is currently in a round, stop the timer and enter upgrade mode
        //if (timeLeftCounter <= 0 && roundActive && !redemptionActive)




        // If the player if done upgrading and the wave is already stopped, start the next wave
        

        // How to start round after upgrade?

        //if (upgradeManager.upgradeDone && !roundStart && mainGameStart)
        {
          //  roundStart = true;
        }

        // If the player has lost all health (called from Player script) and the round is active, stop round and enter redemption mode
       

        

        
    }

    public void StartRound()
    {
        roundCurrent++;
        CheckRound();
        roundCompleteUI.SetActive(true);
        //Instantiate(roundText);
        roundUI.SetActive(true);
        //roundUntilNextUpgrade--;
        
        
       
        //timeLeftCounter = timeLeft;
        
    }

    void CheckRound()
    {
        if (roundStart)
        {
            switch (roundCurrent)
            {
                
                case 1:
                    enemiesToSpawn = 3; //Thirty seconds
                    enemiesOnScreenMax = 1;

                    //roundUntilNextUpgrade = 1; // Tells us in the next round, player will upgrade
                    //upgradeRound = false;
                    break;
                case 2:
                    enemiesToSpawn = 10; // A minute
                    enemiesOnScreenMax = 2;

                    //upgradeRound = true;

                    break;
                case 3:
                    enemiesToSpawn = 15; // A minute and a half
                    enemiesOnScreenMax = 2;

                    break;
                case 4:
                    enemiesToSpawn = 15; //Two minutes
                    enemiesOnScreenMax = 3;
                    break;

                case 5:
                    enemiesToSpawn = 20; //Three minutes
                    enemiesOnScreenMax = 3;
                    break;

                case 6:
                    enemiesToSpawn = 30; //5 Minutes
                    enemiesOnScreenMax = 4;
                    break;

                case 7:
                    enemiesToSpawn = 30; //5 Minutes
                    enemiesOnScreenMax = 4;
                    break;

                case 8:
                    enemiesToSpawn = 30; //5 Minutes
                    enemiesOnScreenMax = 4;
                    break;

                case 9:
                    enemiesToSpawn = 30; //5 Minutes
                    enemiesOnScreenMax = 4;
                    break;

                case 10:
                    enemiesToSpawn = 30; //5 Minutes
                    enemiesOnScreenMax = 4;
                    break;
            }
        }

        if (redemptionStart)
        {
            enemiesToSpawn = 10; //Thirty seconds
            enemiesOnScreenMax = 2;
        }
        

        enemiesDestroyed = 0;
        enemiesOnScreen = 0;

        /*
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
        */
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
        //redemptionUI.SetActive(true);
        playerShield.SetActive(false);
    }

    // Turn on light and give player sword for pre-redemption timing
    void PreRedemption()
    {
        weaponActive.WeaponToActivate("SABER SWORD");
        playerShield.SetActive(false);
        redemptionUI.SetActive(true);
        redemptionLight.SetActive(true);
        redemptionPlatform.SetActive(true);
        CheckRound();
    }

    // Turn stuff off (black-out) for the redemption buffer timer to run down
    void TurnOffForRedemption()
    {
        roundUI.SetActive(false);
        purpleStadium.SetActive(false);
        bluePlatform.SetActive(false);
        platformLight.SetActive(false);
        playerShield.SetActive(false);
        weaponActive.DisableAllWeapons();
    }

    void StopRedemption()
    {
        redemptionPlatform.SetActive(false);
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
        encapsulatingStadium.SetActive(true);
        bluePlatform.SetActive(true);
        purpleStadium.SetActive(true);
        synthCity.SetActive(false);
        playerStartArea.SetActive(false);
        platformLight.SetActive(true);
        gameTitle.SetActive(false);
    }

    void GameReset()
    {
        // Controller Models
        controllerModels[0].SetActive(true);
        controllerModels[1].SetActive(true);

        // Environment
        bluePlatform.SetActive(false);
        purpleStadium.SetActive(false);
        synthCity.SetActive(true);
        playerStartArea.SetActive(true);
        redemptionPlatform.SetActive(false);

        // Lights
        platformLight.SetActive(false);
        redemptionLight.SetActive(false);

        // UI
        roundUI.SetActive(false);
        redemptionUI.SetActive(false);

        // Props
        playerShield.SetActive(false);
    }

    void GameOver()
    {
        // Controller Models
        controllerModels[0].SetActive(true);
        controllerModels[1].SetActive(true);

        // Environment
        bluePlatform.SetActive(false);
        purpleStadium.SetActive(false);
        synthCity.SetActive(false);
        playerStartArea.SetActive(false);

        // Lights
        platformLight.SetActive(false);
        redemptionLight.SetActive(false);

        // UI
        roundUI.SetActive(false);
        redemptionUI.SetActive(false);
        gameOverUI.SetActive(true);

        // Props
        playerShield.SetActive(false);
        weaponActive.DisableAllWeapons();
    }
    
}
