using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private Player playerController;
    private TimeManager timeManager;
    public GameObject playerShield;
    private GameManager gameManager;
    private TutorialManager tutorialManager;
    public WeaponsMenu weaponsMenu;

    public List<MainMenuUI> mainMenuUIList = new List<MainMenuUI>(); //creates a list of menu buttons to access

    public List<WeaponUnlockUI> weaponUnlockUIList = new List<WeaponUnlockUI>();
    public List<WeaponUnlockBoards> weaponUnlockBoardList = new List<WeaponUnlockBoards>(); // list of description boards to access

    public List<AttackUpgradeUI> attackUpgradeUIList = new List<AttackUpgradeUI>();
    public List<AttackUpgradeBoards> attackUpgradeBoardList = new List<AttackUpgradeBoards>();

    public List<DefenseUpgradeUI> defenseUpgradeUIList = new List<DefenseUpgradeUI>();
    public List<DefenseUpgradeBoards> defenseUpgradeBoardList = new List<DefenseUpgradeBoards>();

    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public int currentMainMenuItem; //current menu item for the main menu
    private int oldMainMenuItem; //old menu item for the main menu

    public int currentWeaponUnlockItem;
    public int oldWeaponUnlockItem;

    public int currentAttackUpgradeItem; // To see what is currently highlighted in the attack menu
    public int oldAttackUpgradeItem;

    public int currentDefenseUpgradeItem;
    public int oldDefenseUpgradeItem;

    //public int[] attackUpgradeItem; //Keeps track of what level each upgrade is at

    public bool upgradeSelected;
    public bool upgradeDone;

    public float upgradeTimer;
    public float upgradeTimerCounter;

    private Vector2 touchpad;
    public GameObject upgradeMenu;
    public GameObject mainDial;
    public GameObject weaponMenu;
    public GameObject attackMenu;
    public GameObject defenseMenu;
    public GameObject cursor;
    //public Vector3 cursorPlacement;

    public GameObject upgradeProgress;

    public bool weaponUnlockOpen;
    public bool weaponUnlockActive;

    public bool upgradeMenuOpen;
    public bool upgradeMenuActive;

    public bool attackUpgradeOpen;
    public bool attackUpgradeActive;

    public bool defenseUpgradeOpen;
    public bool defenseUpgradeActive;

    public bool firstPressUp;
    public bool weaponPressUp;
    public bool attackPressUp;
    public bool defensePressUp;

    public bool shieldHide;

    public Material[] menuMat;
    public Material[] itemLevelMat;

    public GameObject blurredProjection;

    public AudioSource subHoverSound;
    public AudioSource itemHoverSound;


    // Use this for initialization
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        

        MenuReset();

        upgradeTimerCounter = upgradeTimer;
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !upgradeMenuOpen && !upgradeSelected && !gameManager.inRedemption)
        {
            upgradeMenuOpen = true;
            upgradeMenuActive = true;
            upgradeDone = false;
            upgradeProgress.SetActive(false);

        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && upgradeMenuOpen)
        {
            firstPressUp = true;
            //print("menuactive");

            if (attackUpgradeOpen)
            {
                attackPressUp = true;
            }

            if (defenseUpgradeOpen)
            {
                defensePressUp = true;
            }

            if (weaponUnlockOpen)
            {
                weaponPressUp = true;
            }

            

        }

        if (upgradeMenuOpen && !upgradeSelected && !gameManager.redemptionPreStart)
        {
            OpenUpgradeMenu();
            //timeManager.DoSlowMotion();
            playerShield.SetActive(false);
            shieldHide = true;
            //blurredProjection.SetActive(true);
        }
        else if (!upgradeMenuOpen && !upgradeSelected)// && gameManager.mainGameStart)// && !gameManager.redemptionPreStart)
        {
            if (gameManager.mainGameStart)
            {
                playerShield.SetActive(true);
                shieldHide = false;
            }

            
           
            //blurredProjection.SetActive(false);

        } else
        {
            //blurredProjection.SetActive(false);

        }

        if (attackUpgradeOpen)
        {
            OpenAttackUpgradeMenu();
        }

        if (weaponUnlockOpen)
        {
            OpenWeaponUnlockMenu();
        }

        if (defenseUpgradeOpen)
        {
            OpenDefenseUpgradeMenu();
        }

        if (upgradeSelected)
        {
            upgradeProgress.SetActive(true);
            MenuReset();

            if (!gameManager.inRedemption)
            {
                upgradeTimerCounter -= Time.deltaTime;
            }
            if (upgradeTimerCounter <= 0)
            {
                upgradeTimerCounter = upgradeTimer;
                upgradeSelected = false;
                upgradeDone = true;
            }
        }


        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    
    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);

        if (upgradeMenuActive)
        {
            cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, touchpad * .125f, Time.unscaledDeltaTime * 10f);
            upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(0f, .1f, .1f), Time.unscaledDeltaTime * 10f);

            mainDial.transform.localPosition = Vector3.Lerp(mainDial.transform.localPosition, Vector3.zero, Time.unscaledDeltaTime * 10f);
            attackMenu.transform.localPosition = Vector3.Lerp(attackMenu.transform.localPosition, new Vector3(-.075f, 0f, 0f), Time.unscaledDeltaTime * 10f);
            weaponMenu.transform.localPosition = Vector3.Lerp(weaponMenu.transform.localPosition, new Vector3(0f, .075f, 0f), Time.unscaledDeltaTime * 10f);
            defenseMenu.transform.localPosition = Vector3.Lerp(defenseMenu.transform.localPosition, new Vector3(.075f, .0f, 0f), Time.unscaledDeltaTime * 10f);

        }
        else if (attackUpgradeActive)
        {
            cursor.transform.localPosition = Vector2.Lerp(cursor.transform.localPosition, new Vector2(-.075f, 0) + (touchpad * .095f), Time.unscaledDeltaTime * 10f);
            upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(.075f, .1f, .1f), Time.unscaledDeltaTime * 10f);

            mainDial.transform.localPosition = Vector3.Lerp(mainDial.transform.localPosition, new Vector3(0f, 0f, .1f), Time.unscaledDeltaTime * 10f);
            weaponMenu.transform.localPosition = Vector3.Lerp(weaponMenu.transform.localPosition, new Vector3(0f, .075f, .1f), Time.unscaledDeltaTime * 10f);
            defenseMenu.transform.localPosition = Vector3.Lerp(defenseMenu.transform.localPosition, new Vector3(.075f, 0f, .1f), Time.unscaledDeltaTime * 10f);

        }
        else if (weaponUnlockActive)
        {
            cursor.transform.localPosition = Vector2.Lerp(cursor.transform.localPosition, new Vector2(0f, .075f) + (touchpad * .095f), Time.unscaledDeltaTime * 10f);
            upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(0f, 0f, .1f), Time.unscaledDeltaTime * 10f);

            mainDial.transform.localPosition = Vector3.Lerp(mainDial.transform.localPosition, new Vector3(0f, 0f, .1f), Time.unscaledDeltaTime * 10f);
            attackMenu.transform.localPosition = Vector3.Lerp(attackMenu.transform.localPosition, new Vector3(-.075f, 0f, .1f), Time.unscaledDeltaTime * 10f);
            defenseMenu.transform.localPosition = Vector3.Lerp(defenseMenu.transform.localPosition, new Vector3(.075f, 0f, .1f), Time.unscaledDeltaTime * 10f);

        } else if (defenseUpgradeActive)
        {
            cursor.transform.localPosition = Vector2.Lerp(cursor.transform.localPosition, new Vector2(.075f, 0) + (touchpad * .095f), Time.unscaledDeltaTime * 10f);
            upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(-.075f, .1f, .1f), Time.unscaledDeltaTime * 10f);

            mainDial.transform.localPosition = Vector3.Lerp(mainDial.transform.localPosition, new Vector3(0f, 0f, .1f), Time.unscaledDeltaTime * 10f);
            attackMenu.transform.localPosition = Vector3.Lerp(attackMenu.transform.localPosition, new Vector3(-.075f, 0f, .1f), Time.unscaledDeltaTime * 10f);
            weaponMenu.transform.localPosition = Vector3.Lerp(weaponMenu.transform.localPosition, new Vector3(0f, .075f, .1f), Time.unscaledDeltaTime * 10f);

        }

        touchpad.x = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        touchpad.y = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        Vector2 fromVector2 = new Vector2(0, 1);
        Vector2 toVector2 = touchpad;

        angleFromCenter = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
        {
            angleFromCenter = 360 - angleFromCenter;
        }

        if (firstPressUp && !attackPressUp  && !weaponPressUp && !defensePressUp)//  && !attackUpgradeOpen && !weaponUnlockOpen && !defenseUpgradeOpen)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)// && !attackUpgradeOpen && !weaponUnlockOpen && !defenseUpgradeOpen)
            {
                //print("called");
                // OPEN WEAPON UNLOCK MENU
                if ((340 < angleFromCenter || angleFromCenter <= 20) && !attackUpgradeOpen && !defenseUpgradeOpen)
                {
                    currentMainMenuItem = 0;

                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))// && !attackUpgradeOpen && !weaponUnlockOpen && !defenseUpgradeOpen)
                    {
                        weaponUnlockOpen = true;
                        weaponUnlockActive = true;
                        upgradeMenuActive = false;

                        StartCoroutine(ButtonPressHaptics(1000));
                        subHoverSound.Play();
                    }

                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && weaponUnlockOpen)
                    {
                        weaponPressUp = true;

                    }
                }
                // OPEN ATTACK UPGRADES MENU
                else if (250 < angleFromCenter && angleFromCenter <= 290 && !weaponUnlockOpen && !defenseUpgradeOpen)
                {
                    currentMainMenuItem = 1;

                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        attackUpgradeOpen = true;
                        attackUpgradeActive = true;
                        upgradeMenuActive = false;
                        subHoverSound.Play();
                        StartCoroutine(ButtonPressHaptics(1000));

                        /*if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && attackUpgradeOpen)
                        {
                            attackPressUp = true;
                        }*/

                    }

                    /*if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && attackUpgradeOpen)
                    {
                        attackPressUp = true;


                    }*/

                }
                // OPEN DEFENSE UPGRADES MENU
                else if (70 < angleFromCenter && angleFromCenter <= 110 && !attackUpgradeOpen && !weaponUnlockOpen)
                {
                    currentMainMenuItem = 2;

                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        defenseUpgradeOpen = true;
                        defenseUpgradeActive = true;
                        upgradeMenuActive = false;
                        subHoverSound.Play();
                        StartCoroutine(ButtonPressHaptics(1000));
                    }

                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && defenseUpgradeOpen)
                    {
                        defensePressUp = true;


                    }
                }
            }
            // CLOSE UPGRADE MAIN MENU
            else if(!attackUpgradeOpen && !weaponUnlockOpen && !defenseUpgradeOpen)
            {
                currentMainMenuItem = 3;

                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    MenuReset();
                }
            }
        }

        if (currentMainMenuItem != oldMainMenuItem)
        {
            StartCoroutine(ButtonPressHaptics(300));
            itemHoverSound.Play();
            mainMenuUIList[oldMainMenuItem].sphere.GetComponent<Renderer>().material = menuMat[0];
            oldMainMenuItem = currentMainMenuItem;
            mainMenuUIList[currentMainMenuItem].sphere.GetComponent<Renderer>().material = menuMat[1];

        }
    }
    
    
    void OpenWeaponUnlockMenu()
    {
        if (weaponUnlockActive && !attackUpgradeActive && !defenseUpgradeActive && !upgradeSelected)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                // PISTOL
                if (259 < angleFromCenter && angleFromCenter <= 281)
                {
                    currentWeaponUnlockItem = 0;
                }
                // RIFLE
                else if (304 < angleFromCenter && angleFromCenter <= 326)
                {
                    currentWeaponUnlockItem = 1;
                }
                // SHOTGUN
                else if (349 < angleFromCenter || angleFromCenter <= 11)
                {
                    currentWeaponUnlockItem = 2;
                }
                // SABER SWORD
                else if (34 < angleFromCenter && angleFromCenter <= 56)
                {
                    currentWeaponUnlockItem = 3;
                }
                // HYPER RIFLE
                else if (79 < angleFromCenter && angleFromCenter <= 101)
                {
                    currentWeaponUnlockItem = 4;
                }
                else
                {
                    /*foreach (WeaponUnlockBoards weapon in weaponUnlockBoardList)
                    {
                        weapon.weaponUpgrade.SetActive(false);
                    }*/

                    //currentWeaponUnlockItem = -1;
                }
            }
            // BACK BUTTON TO MAIN MENU
            else
            {
                currentWeaponUnlockItem = 5; // Back button is highlighted
            }
        }

        if (currentWeaponUnlockItem != oldWeaponUnlockItem && currentWeaponUnlockItem >= 0)
        {
            StartCoroutine(ButtonPressHaptics(300));
            itemHoverSound.Play();
            /*if (currentWeaponUnlockItem >= 0 && currentWeaponUnlockItem < 5)
            {
                weaponUnlockBoardList[oldWeaponUnlockItem].weaponUpgrade.SetActive(false);
                weaponUnlockBoardList[currentWeaponUnlockItem].weaponUpgrade.SetActive(true);
                //weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition = Vector3.Lerp(weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition, new Vector3(weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition.x, weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition.y, -0.45f), Time.unscaledDeltaTime * 10f);
            }*/

            weaponUnlockUIList[oldWeaponUnlockItem].sphere.GetComponent<Renderer>().material = menuMat[2];

            if (oldWeaponUnlockItem < 5)
            {
                weaponUnlockBoardList[oldWeaponUnlockItem].weaponBoards[weaponUnlockBoardList[oldWeaponUnlockItem].level].SetActive(false);
            }

            oldWeaponUnlockItem = currentWeaponUnlockItem;

            if (currentWeaponUnlockItem == 5)
            {
                weaponUnlockUIList[currentWeaponUnlockItem].sphere.GetComponent<Renderer>().material = menuMat[1];
            } else
            {
                weaponUnlockUIList[currentWeaponUnlockItem].sphere.GetComponent<Renderer>().material = itemLevelMat[currentWeaponUnlockItem];
            }
           
        }

        if (currentWeaponUnlockItem < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                weaponUnlockUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
            }
        }

        
        if (currentWeaponUnlockItem >= 0 && currentWeaponUnlockItem < 5 && oldWeaponUnlockItem < 5)
        {
            weaponUnlockBoardList[currentWeaponUnlockItem].weaponBoards[weaponUnlockBoardList[currentWeaponUnlockItem].level].SetActive(true);
            //weaponUnlockBoardList[oldWeaponUnlockItem].weaponUpgrade.SetActive(false);

            //weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition = Vector3.Lerp(weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition, new Vector3(weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition.x, weaponUnlockUIList[currentWeaponUnlockItem].sphere.transform.localPosition.y, -0.45f), Time.unscaledDeltaTime * 10f);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && weaponPressUp && weaponUnlockActive && !attackUpgradeActive && !upgradeMenuActive && !defenseUpgradeActive && !upgradeSelected)
        {
            if (currentWeaponUnlockItem >= 0 && currentWeaponUnlockItem < 5 && weaponUnlockBoardList[currentWeaponUnlockItem].level < 1)
            {
                if (weaponUnlockBoardList[currentWeaponUnlockItem].weaponBoards[weaponUnlockBoardList[currentWeaponUnlockItem].level].GetComponent<Upgrades>().upgradeCost <= playerController.playerBullets)
                {
                    // Applies upgrade effect after upgrade timer
                    StartCoroutine(ApplyUpgrade(weaponUnlockBoardList[currentWeaponUnlockItem].weaponBoards[weaponUnlockBoardList[currentWeaponUnlockItem].level].GetComponent<Upgrades>()));
                    playerController.playerBullets -= weaponUnlockBoardList[currentWeaponUnlockItem].weaponBoards[weaponUnlockBoardList[currentWeaponUnlockItem].level].GetComponent<Upgrades>().upgradeCost;

                    weaponUnlockBoardList[currentWeaponUnlockItem].level++;

                    upgradeSelected = true;
                }
            }
            else if (currentWeaponUnlockItem == 5)
            {
                CloseWeaponUnlockMenu();
                subHoverSound.Play();

            }
        }
    }
    

    void OpenAttackUpgradeMenu()
    {
        if (attackUpgradeActive && !weaponUnlockActive && !defenseUpgradeActive && !upgradeSelected)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                // FIRE RATE UPGRADES
                if (349 < angleFromCenter || angleFromCenter <= 11)
                {
                    currentAttackUpgradeItem = 0;
                }
                // DAMAGE UPGRADES
                else if (304 < angleFromCenter && angleFromCenter <= 326)
                {
                    currentAttackUpgradeItem = 1;
                }
                // ACCURACY UPGRADES
                else if (259 < angleFromCenter && angleFromCenter <= 281)
                {
                    currentAttackUpgradeItem = 2;
                }
                // BULLET CAPACITY UPGRADES
                else if (214 < angleFromCenter && angleFromCenter <= 236)
                {
                    currentAttackUpgradeItem = 3;
                }
                // BULLETS PER KILL UPGRADES
                else if (169 < angleFromCenter && angleFromCenter <= 191)
                {
                    currentAttackUpgradeItem = 4;
                }
                else
                {
                    /*foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
                    {
                        foreach (GameObject obj in board.levelBoards)
                        {
                            obj.SetActive(false);
                        }
                    }

                    currentAttackUpgradeItem = -1;
                    */
                }
            }
            // BACK BUTTON TO MAIN MENU
            else
            {
                currentAttackUpgradeItem = 5; // Back button is highlighted
            }
        }

        if (currentAttackUpgradeItem != oldAttackUpgradeItem && currentAttackUpgradeItem >= 0)
        {
            StartCoroutine(ButtonPressHaptics(300));
            itemHoverSound.Play();


            if (oldAttackUpgradeItem < 5)
            {
                attackUpgradeBoardList[oldAttackUpgradeItem].levelBoards[attackUpgradeBoardList[oldAttackUpgradeItem].level].SetActive(false);
            }

            attackUpgradeUIList[oldAttackUpgradeItem].sphere.GetComponent<Renderer>().material = menuMat[2];
            oldAttackUpgradeItem = currentAttackUpgradeItem;

            if (currentAttackUpgradeItem == 5)
            {
                attackUpgradeUIList[5].sphere.GetComponent<Renderer>().material = menuMat[1];
            }
            else
            {
                attackUpgradeUIList[currentAttackUpgradeItem].sphere.GetComponent<Renderer>().material = itemLevelMat[attackUpgradeBoardList[currentAttackUpgradeItem].level];
                //print(itemLevelMat[attackUpgradeBoardList[currentAttackUpgradeItem].level]);
            }

            //attackUpgradeUIList[oldAttackUpgradeItem].sceneImage.color = attackUpgradeUIList[oldAttackUpgradeItem].normalColor;
            //oldAttackUpgradeItem = currentAttackUpgradeItem;
            //attackUpgradeUIList[currentAttackUpgradeItem].sceneImage.color = attackUpgradeUIList[currentAttackUpgradeItem].highlightColor;
        }

        if (currentAttackUpgradeItem < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                attackUpgradeUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
            }
        }

        if (currentAttackUpgradeItem >= 0 && currentAttackUpgradeItem < 5)
        {
            attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards[attackUpgradeBoardList[currentAttackUpgradeItem].level].SetActive(true);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && attackPressUp && attackUpgradeActive && attackUpgradeOpen && !weaponUnlockActive && !upgradeMenuActive && !defenseUpgradeActive && !upgradeSelected)
        {
            //print("called");
            if (currentAttackUpgradeItem >= 0 && currentAttackUpgradeItem < 5)
            {
                if (attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards[attackUpgradeBoardList[currentAttackUpgradeItem].level].GetComponent<Upgrades>().upgradeCost <= playerController.playerBullets)
                {
                    // Applies upgrade effect after upgrade timer
                    StartCoroutine(ApplyUpgrade(attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards[attackUpgradeBoardList[currentAttackUpgradeItem].level].GetComponent<Upgrades>()));

                    playerController.playerBullets -= attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards[attackUpgradeBoardList[currentAttackUpgradeItem].level].GetComponent<Upgrades>().upgradeCost;

                    upgradeSelected = true;

                    attackUpgradeBoardList[currentAttackUpgradeItem].level++;
                    if (attackUpgradeBoardList[currentAttackUpgradeItem].level > (attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards.Length - 1))
                    {
                        attackUpgradeBoardList[currentAttackUpgradeItem].level = attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards.Length - 1;
                    }
                }
            }
            else if (currentAttackUpgradeItem == 5)
            {
                CloseAttackUpgradeMenu();
                subHoverSound.Play();

            }
        }
    }

    void OpenDefenseUpgradeMenu()
    {
        if (defenseUpgradeActive && !weaponUnlockActive && !attackUpgradeActive && !upgradeSelected)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                // FIRE RATE UPGRADES
                if (349 < angleFromCenter || angleFromCenter <= 11)
                {
                    currentDefenseUpgradeItem = 0;
                }
                // DAMAGE UPGRADES
                else if (34 < angleFromCenter && angleFromCenter <= 56)
                {
                    currentDefenseUpgradeItem = 1;
                }
                // ACCURACY UPGRADES
                else if (79 < angleFromCenter && angleFromCenter <= 101)
                {
                    currentDefenseUpgradeItem = 2;
                }
                // BULLET CAPACITY UPGRADES
                else if (124 < angleFromCenter && angleFromCenter <= 146)
                {
                    currentDefenseUpgradeItem = 3;
                }
                // BULLETS PER KILL UPGRADES
                else if (169 < angleFromCenter && angleFromCenter <= 191)
                {
                    currentDefenseUpgradeItem = 4;
                }
                else
                {
                    /*foreach (DefenseUpgradeBoards board in defenseUpgradeBoardList)
                    {
                        foreach (GameObject obj in board.levelBoards)
                        {
                            obj.SetActive(false);
                        }
                    }

                    currentDefenseUpgradeItem = -1;
                    */
                }
            }
            // BACK BUTTON TO MAIN MENU
            else
            {
                currentDefenseUpgradeItem = 5; // Back button is highlighted
            }
        }

        if (currentDefenseUpgradeItem != oldDefenseUpgradeItem && currentDefenseUpgradeItem >= 0)
        {
            StartCoroutine(ButtonPressHaptics(300));
            itemHoverSound.Play();


            if (oldDefenseUpgradeItem < 5)
            {
                defenseUpgradeBoardList[oldDefenseUpgradeItem].levelBoards[defenseUpgradeBoardList[oldDefenseUpgradeItem].level].SetActive(false);

            }

            defenseUpgradeUIList[oldDefenseUpgradeItem].sphere.GetComponent<Renderer>().material = menuMat[2];
            oldDefenseUpgradeItem = currentDefenseUpgradeItem;

            if (currentDefenseUpgradeItem == 5)
            {
                defenseUpgradeUIList[currentDefenseUpgradeItem].sphere.GetComponent<Renderer>().material = menuMat[1];
            }
            else
            {
                defenseUpgradeUIList[currentDefenseUpgradeItem].sphere.GetComponent<Renderer>().material = itemLevelMat[defenseUpgradeBoardList[currentDefenseUpgradeItem].level];
            }


        }

        if (currentDefenseUpgradeItem < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                defenseUpgradeUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
            }
        }

        if (currentDefenseUpgradeItem >= 0 && currentDefenseUpgradeItem < 5)
        {
            defenseUpgradeBoardList[currentDefenseUpgradeItem].levelBoards[defenseUpgradeBoardList[currentDefenseUpgradeItem].level].SetActive(true);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && defensePressUp && defenseUpgradeActive && !attackUpgradeActive && !weaponUnlockActive && !upgradeMenuActive && !upgradeSelected)
        {
            if (currentDefenseUpgradeItem >= 0 && currentDefenseUpgradeItem < 5)
            {
                if (defenseUpgradeBoardList[currentDefenseUpgradeItem].levelBoards[defenseUpgradeBoardList[currentDefenseUpgradeItem].level].GetComponent<Upgrades>().upgradeCost <= playerController.playerBullets)
                {
                    // Applies upgrade effect after upgrade timer
                    StartCoroutine(ApplyUpgrade(defenseUpgradeBoardList[currentDefenseUpgradeItem].levelBoards[defenseUpgradeBoardList[currentDefenseUpgradeItem].level].GetComponent<Upgrades>()));

                    playerController.playerBullets -= defenseUpgradeBoardList[currentDefenseUpgradeItem].levelBoards[defenseUpgradeBoardList[currentDefenseUpgradeItem].level].GetComponent<Upgrades>().upgradeCost;

                    upgradeSelected = true;

                    defenseUpgradeBoardList[currentDefenseUpgradeItem].level++;
                    if (defenseUpgradeBoardList[currentDefenseUpgradeItem].level > (defenseUpgradeBoardList[currentDefenseUpgradeItem].levelBoards.Length - 1))
                    {
                        defenseUpgradeBoardList[currentDefenseUpgradeItem].level = defenseUpgradeBoardList[currentDefenseUpgradeItem].levelBoards.Length - 1;
                    }
                }
            }
            else if (currentDefenseUpgradeItem == 5)
            {
                CloseDefenseUpgradeMenu();
                subHoverSound.Play();

            }
        }
    }

    void CloseAttackUpgradeMenu()
    {
        attackPressUp = false;
        attackUpgradeOpen = false;
        attackUpgradeActive = false;

        upgradeMenuActive = true;

        for (int i = 0; i < 5; i++)
        {
            attackUpgradeUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
            //attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].unavailableColor;
        }

        foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
        {
            foreach (GameObject obj in board.levelBoards)
            {
                obj.SetActive(false);
            }
        }

        //cursor.transform.localPosition = new Vector2(0f, 0f);
        //print("go back");
        //upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(0f, .1f, .1f), Time.unscaledDeltaTime * 2f);
    }

    void CloseWeaponUnlockMenu()
    {
        weaponPressUp = false;
        weaponUnlockOpen = false;
        weaponUnlockActive = false;

        upgradeMenuActive = true;

        for (int i = 0; i < 5; i++)
        {
            weaponUnlockUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
        }

        foreach (WeaponUnlockBoards board in weaponUnlockBoardList)
        {
            foreach (GameObject obj in board.weaponBoards)
            {
                obj.SetActive(false);
            }
        }

        //cursor.transform.localPosition = new Vector2(0f, 0f);
        //upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(0f, .1f, .1f), Time.unscaledDeltaTime * 2f);
    }

    void CloseDefenseUpgradeMenu()
    {
        defensePressUp = false;
        defenseUpgradeOpen = false;
        defenseUpgradeActive = false;

        upgradeMenuActive = true;

        for (int i = 0; i < 5; i++)
        {
            defenseUpgradeUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
        }

        foreach (DefenseUpgradeBoards board in defenseUpgradeBoardList)
        {
            foreach (GameObject obj in board.levelBoards)
            {
                obj.SetActive(false);
            }
        }

        //cursor.transform.localPosition = new Vector2(0f, 0f);
        //upgradeMenu.transform.localPosition = Vector3.Lerp(upgradeMenu.transform.localPosition, new Vector3(0f, .1f, .1f), Time.unscaledDeltaTime * 2f);
    }

    IEnumerator ApplyUpgrade(Upgrades upgrade)
    {
        yield return new WaitForSeconds(upgradeTimer);
        upgrade.AddUpgradeEffect();
    }

    IEnumerator ButtonPressHaptics(float strength)
    {
        device.TriggerHapticPulse((ushort)strength);
        yield return new WaitForSeconds(.1f);

    }

    void MenuReset()
    {
        upgradeMenuOpen = false;
        upgradeMenuActive = false;

        attackUpgradeOpen = false;
        attackUpgradeActive = false;

        defenseUpgradeOpen = false;
        defenseUpgradeActive = false;

        weaponUnlockOpen = false;
        weaponUnlockActive = false;

        firstPressUp = false;
        weaponPressUp = false;
        attackPressUp = false;
        defensePressUp = false;

        shieldHide = true;

        currentMainMenuItem = 0;
        oldMainMenuItem = 1;

        currentAttackUpgradeItem = 0;
        oldAttackUpgradeItem = 1;

        currentDefenseUpgradeItem = 0;
        oldDefenseUpgradeItem = 1;

        currentWeaponUnlockItem = 0;
        oldWeaponUnlockItem = 1;



        // Make all extending UI unavailable
        for (int i = 0; i < 5; i++)
        {
            weaponUnlockUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2]; ;
        }

        for (int i = 0; i < 5; i++)
        {
            attackUpgradeUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
            //attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].unavailableColor;
        }

        for (int i = 0; i < 5; i++)
        {
            defenseUpgradeUIList[i].sphere.GetComponent<Renderer>().material = menuMat[2];
        }

        // Make all extending description boards unavailable
        foreach (WeaponUnlockBoards board in weaponUnlockBoardList)
        {
            foreach (GameObject obj in board.weaponBoards)
            {
                obj.SetActive(false);
            }
        }

        foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
        {
            foreach (GameObject obj in board.levelBoards)
            {
                obj.SetActive(false);
            }
        }

        foreach (DefenseUpgradeBoards board in defenseUpgradeBoardList)
        {
            foreach (GameObject obj in board.levelBoards)
            {
                obj.SetActive(false);
            }
        }

        cursor.transform.localPosition = new Vector2(0f, 0f);
        upgradeMenu.transform.localPosition = new Vector3(0f, .1f, .1f);

        upgradeMenu.SetActive(false);
        blurredProjection.SetActive(false);


    }

    [System.Serializable]
    public class MainMenuUI
    {
        public string name;
        public GameObject sphere;
        //public bool hasWeapon;
        //public AudioClip recording;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }

    [System.Serializable]
    public class WeaponUnlockUI
    {
        public string name;
        public GameObject sphere;
        //public bool hasWeapon;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }

    [System.Serializable]
    public class WeaponUnlockBoards
    {
        public string name;
        public int level;
        public GameObject[] weaponBoards;
    }

    [System.Serializable]
    public class AttackUpgradeUI
    {
        public string name;
        //public bool hasWeapon;
        //public AudioClip recording;
        //public Image sceneImage;
        public GameObject sphere;
        //public Material normalMat;
        //public Material highlightMat;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }

    [System.Serializable]
    public class AttackUpgradeBoards
    {
        public string name;
        public int level;
        public GameObject[] levelBoards;
    }

    [System.Serializable]
    public class DefenseUpgradeUI
    {
        public string name;
        public GameObject sphere;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;
    }

    [System.Serializable]
    public class DefenseUpgradeBoards
    {
        public string name;
        public int level;
        public GameObject[] levelBoards;
    }
}