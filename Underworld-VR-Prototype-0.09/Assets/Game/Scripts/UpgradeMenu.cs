using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    //public GameObject[][] attackUpgradeBoards = new GameObject[5][];

    private Player playerController;

    public List<MainMenuUI> mainMenuUIList = new List<MainMenuUI>(); //creates a list of menu buttons to access

    public List<AttackUpgradeUI> attackUpgradeUIList = new List<AttackUpgradeUI>(); //creates a list of menu buttons to access
    public List<AttackUpgradeBoards> attackUpgradeBoardList = new List<AttackUpgradeBoards>(); //creates a list of menu buttons to access


    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public int currentMainMenuItem; //current menu item for the main menu
    private int oldMainMenuItem; //old menu item for the main menu

    public int currentAttackMenuItem; // To see what is currently highlighted in the attack menu
    public int oldAttackMenuItem;

    //public int[] attackUpgradeItem; //Keeps track of what level each upgrade is at

    public int currentDefenseUpgradeItem;
    public int oldDefenseUpgradeItem;

    public int currentWeaponUpgradeItem;
    public int oldWeaponUpgradeItem;

    public bool upgradeSelected;
    public bool upgradeDone;
    public float upgradeTimer;
    public float upgradeTimerCounter;

    private Vector2 touchpad;
    public GameObject upgradeMenu;

    public GameObject cursor;
    public Vector3 cursorPlacement;

    public GameObject upgradeProgress;

    public bool upgradeMenuOpen;
    public bool upgradeMenuActive;

    public bool attackUpgradeOpen;
    public bool attackUpgradeActive;

    public bool weaponUpgradeOpen;
    public bool weaponUpgradeActive;

    public bool defenseUpgradeOpen;
    public bool defenseUpgradeActive;

    public GameObject[] attackUpgradeUI;

    //  Attack Upgrade UI and Levels
    //public GameObject[] fireRateUpgradeBoard;
    //public int fireRateUpgradeLevel;

    //public GameObject[] damageUpgradeBoard;
    //public int[] damageUpgradeLevel;

    //public GameObject[] accuracyUpgradeBoard;
    //public int[] accuracyUpgradeLevel;

    //public GameObject[] bulletCapacityUpgradeBoard;
    //public int[] bulletCapacityUpgradeLevel;


    //public GameObject[] attackUpgradeBoards;
    //public GameObject attackMenuItem;


    public GameObject[] defenseUpgradesUI;
    public GameObject[] weaponUpgradeUI;


    


    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        MenuReset();
        //attackMenuItem = null;
        //Instantiate(attackMenuItem, transform.position, Quaternion.identity);

        upgradeTimerCounter = upgradeTimer;
		
	}

    void Update()
    {
        if (upgradeMenuOpen && !upgradeSelected)
        {
            OpenUpgradeMenu();
        }

        if (attackUpgradeOpen)
        {
            OpenAttackUpgradeMenu();
        }

        if (upgradeSelected)
        {
            upgradeProgress.SetActive(true);
            MenuReset();
               
            upgradeTimerCounter -= Time.deltaTime;
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

        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !upgradeMenuOpen && !upgradeSelected)
        {
            upgradeMenuOpen = true;
            upgradeDone = false;
            upgradeProgress.SetActive(false);
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && upgradeMenuOpen && !upgradeSelected && !attackUpgradeActive && !defenseUpgradeActive && !weaponUpgradeActive)
        {
            upgradeMenuActive = true;
        }



        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && upgradeMenuOpen && upgradeMenuActive)
        {
            //MenuReset();
        }

        


    }


    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);

        if (upgradeMenuActive)
        {
            cursor.transform.localPosition = touchpad * .125f;
        } else if (attackUpgradeActive)
        {
            cursor.transform.localPosition = new Vector2(-.1f, 0) + (touchpad * .07f);
            upgradeMenu.transform.localPosition = new Vector3(.1f, .1f, .1f);
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

        if (upgradeMenuActive && !attackUpgradeActive && !defenseUpgradeActive && !weaponUpgradeActive)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                //map angle from center to specific buttons;
                if (340 < angleFromCenter || angleFromCenter <= 20)
                {
                    currentMainMenuItem = 0; //Weapon Upgrades
                                             //print("Weapon upgrades");


                }
                // Attack Upgrade Menu
                else if (250 < angleFromCenter && angleFromCenter <= 290)
                {
                    currentMainMenuItem = 1;

                    if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        attackUpgradeOpen = true;
                    }

                    if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
                    {
                        attackUpgradeActive = true;
                        upgradeMenuActive = false;
                    }

                }
                else if (70 < angleFromCenter && angleFromCenter <= 110)
                {
                    currentMainMenuItem = 2; //Defense Upgrades
                                             //print("defense upgrades");

                }
            } else
            {
                currentMainMenuItem = 3; // button to close main menu

                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    MenuReset();
                }

            }
        }

        if (currentMainMenuItem != oldMainMenuItem)
        {
            mainMenuUIList[oldMainMenuItem].sceneImage.color = mainMenuUIList[oldMainMenuItem].normalColor;
            oldMainMenuItem = currentMainMenuItem;
            mainMenuUIList[currentMainMenuItem].sceneImage.color = mainMenuUIList[currentMainMenuItem].highlightColor;
            //print("changing color");
        }






    }

    void UpgradeSelected()
    {

    }

    void MenuReset()
    {

        upgradeMenuOpen = false;
        upgradeMenuActive = false;
        attackUpgradeOpen = false;
        attackUpgradeActive = false;
        defenseUpgradeOpen = false;
        defenseUpgradeActive = false;
        weaponUpgradeOpen = false;
        weaponUpgradeActive = false;

        currentMainMenuItem = 0;
        oldMainMenuItem = 1;

        currentAttackMenuItem = 0;
        oldAttackMenuItem = 1;

        currentDefenseUpgradeItem = 0;
        oldDefenseUpgradeItem = 1;

        currentWeaponUpgradeItem = 0;
        oldWeaponUpgradeItem = 1;

        for (int i = 0; i < 5; i++)
        {
            attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].unavailableColor;
        }


        /*foreach (GameObject upgrade in attackUpgradeUI)
        {
            upgrade.SetActive(false);
        }*/

        foreach (GameObject upgrade in defenseUpgradesUI)
        {
            upgrade.SetActive(false);
        }

        foreach (GameObject upgrade in weaponUpgradeUI)
        {
            upgrade.SetActive(false);
        }

        foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
        {
            foreach (GameObject obj in board.levelBoards)
            {
                obj.SetActive(false);
            }
        }

        cursor.transform.localPosition = new Vector2(0f, 0f);
        upgradeMenu.transform.localPosition = new Vector3(0f, .1f, .1f);

        upgradeMenu.SetActive(false);

    }

    void OpenAttackUpgradeMenu()
    {
        //attackMenuItem.SetActive(true);

        foreach (GameObject upgrade in attackUpgradeUI)
        {
            upgrade.SetActive(true);
        }

        if (attackUpgradeActive && !weaponUpgradeActive && !defenseUpgradeActive && !upgradeSelected)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                // Fire Rate
                if (349 < angleFromCenter || angleFromCenter <= 11)
                {
                    currentAttackMenuItem = 0; // To see what is currently highlighted in the attack menu


                    //fireRateUpgradeBoard[attackUpgradeItem[currentAttackMenuItem]].SetActive(true);
                    //attackUpgradeBoards[attackUpgradeItem[currentAttackMenuItem]].SetActive(true);
                    //print("attack 1");
                }
                else if (304 < angleFromCenter && angleFromCenter <= 326)
                {
                    currentAttackMenuItem = 1; //Damage
                                               //attackUpgradeBoards[1].SetActive(true);
                                               //print("attack 2");
                }
                else if (259 < angleFromCenter && angleFromCenter <= 281)
                {
                    currentAttackMenuItem = 2; //Accuracy

                    //print(attackUpgradeBoardList[currentAttackMenuItem].level);
                    //attackUpgradeBoards[2].SetActive(true);
                    //print("attack 3");
                }
                else if (214 < angleFromCenter && angleFromCenter <= 236)
                {
                    currentAttackMenuItem = 3; //Bullet Capacity
                                               //attackUpgradeBoards[3].SetActive(true);
                                               //print("attack 4");

                }
                else if (169 < angleFromCenter && angleFromCenter <= 191)
                {
                    currentAttackMenuItem = 4; //Bullets Earned
                                               //attackUpgradeBoards[4].SetActive(true);
                                               //print("attack 5");
                }
                else
                {
                    foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
                    {
                        foreach (GameObject obj in board.levelBoards)
                        {
                            obj.SetActive(false);
                        }
                    }

                    //attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].SetActive(false);

                    currentAttackMenuItem = -1;
                }
            } else
            {
                currentAttackMenuItem = 5; // Back button is highlighted
            }
            
        }



        if (currentAttackMenuItem != oldAttackMenuItem && currentAttackMenuItem >= 0)
        {
            attackUpgradeUIList[oldAttackMenuItem].sceneImage.color = attackUpgradeUIList[oldAttackMenuItem].normalColor;
            oldAttackMenuItem = currentAttackMenuItem;
            attackUpgradeUIList[currentAttackMenuItem].sceneImage.color = attackUpgradeUIList[currentAttackMenuItem].highlightColor;
            //print("changing color");
        }

        if (currentAttackMenuItem < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].normalColor;
            }
        }

        //print(attackUpgradeBoardList[currentAttackMenuItem].level);

        if (currentAttackMenuItem >= 0 && currentAttackMenuItem < 5)
        {
            attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].SetActive(true);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && attackUpgradeActive && !weaponUpgradeActive && !upgradeMenuActive && !defenseUpgradeActive && !upgradeSelected)
        {
            if (currentAttackMenuItem >= 0 && currentAttackMenuItem < 5)
            {
                if (attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>().upgradeCost <= playerController.playerBullets)
                {
                    // Applies upgrade effect after upgrade timer
                    StartCoroutine(ApplyUpgrade(attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>()));

                    //print(attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level]);

                    playerController.playerBullets -= attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>().upgradeCost;

                    upgradeSelected = true;

                    //attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>().AddUpgradeEffect();

                    attackUpgradeBoardList[currentAttackMenuItem].level++;
                    if (attackUpgradeBoardList[currentAttackMenuItem].level > (attackUpgradeBoardList[currentAttackMenuItem].levelBoards.Length - 1))
                    {
                        attackUpgradeBoardList[currentAttackMenuItem].level = attackUpgradeBoardList[currentAttackMenuItem].levelBoards.Length - 1;
                    }
                }
            } else if (currentAttackMenuItem == 5)
            {
                CloseAttackUpgradeMenu();
            }
        }
    }

    void CloseAttackUpgradeMenu()
    {
        attackUpgradeOpen = false;
        attackUpgradeActive = false;

        upgradeMenuActive = true;

        for (int i = 0; i < 5; i++)
        {
            attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].unavailableColor;
        }

        cursor.transform.localPosition = new Vector2(0f, 0f);
        upgradeMenu.transform.localPosition = new Vector3(0f, .1f, .1f);
    }

    IEnumerator ApplyUpgrade(Upgrades upgrade)
    {
        yield return new WaitForSeconds(upgradeTimer);
        upgrade.AddUpgradeEffect();
        //print("upgrade applied");
    }

    void OpenDefenseUpgradeMenu()
    {

    }

    void OpenWeaponUpgradeMenu()
    {

    }

    [System.Serializable]
    public class MainMenuUI
    {
        public string name;
        //public bool hasWeapon;
        //public AudioClip recording;
        public Image sceneImage;
        public Color normalColor = Color.white;
        public Color highlightColor = Color.grey;
        public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }


    [System.Serializable]
    public class AttackUpgradeUI
    {
        public string name;
        //public bool hasWeapon;
        //public AudioClip recording;
        public Image sceneImage;
        public Color normalColor = Color.white;
        public Color highlightColor = Color.grey;
        public Color pressedColor = Color.yellow;
        public Color unavailableColor = Color.black;

    }

    [System.Serializable]
    public class AttackUpgradeBoards
    {
        public string name;
        public int level;
        public GameObject[] levelBoards;
        //public float levelUpTime;
        //public bool hasWeapon;
        //public AudioClip recording;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }
}
