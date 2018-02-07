using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    //public GameObject[][] attackUpgradeBoards = new GameObject[5][];

    private Player playerController;

    public List<AttackUpgradeUI> upgradeUIList = new List<AttackUpgradeUI>(); //creates a list of menu buttons to access

    public List<AttackUpgradeBoards> attackUpgradeBoardList = new List<AttackUpgradeBoards>(); //creates a list of menu buttons to access


    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public int currentMainMenuItem; //current menu item for the main menu
    private int oldMainMenuItem; //old menu item for the main menu

    public int currentAttackMenuItem; // To see what is currently highlighted in the attack menu
    public int oldAttackMenuItem;

    public int[] attackUpgradeItem; //Keeps track of what level each upgrade is at

    public int currentDefenseUpgradeItem;
    public int oldDefenseUpgradeItem;

    public int currentWeaponUpgradeItem;
    public int oldWeaponUpgradeItem;

    private bool upgradeSelected;

    private Vector2 touchpad;
    public GameObject upgradeMenu;

    public GameObject cursor;

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
    public GameObject[] fireRateUpgradeBoard;
    public int fireRateUpgradeLevel;

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
		
	}

    void Update()
    {
        if (upgradeMenuOpen)
        {
            OpenUpgradeMenu();
        }

        if (attackUpgradeOpen)
        {
            OpenAttackUpgradeMenu();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            upgradeMenuOpen = true;
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && upgradeMenuOpen)
        {

        }

        


    }


    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);

        cursor.transform.localPosition = touchpad * .1f;

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

        if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f && upgradeMenuActive && !attackUpgradeActive && !defenseUpgradeActive && !weaponUpgradeActive)
        {
            //map angle from center to specific buttons;
            if (340 < angleFromCenter || angleFromCenter <= 20)
            {
                currentMainMenuItem = 0; //Weapon Upgrades
                //print("Weapon upgrades");


            }
            else if (250 < angleFromCenter && angleFromCenter <= 290)
            {
                currentMainMenuItem = 1; //Attack Upgrades
                //print("attack upgrades");

                if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    attackUpgradeOpen = true;
                    attackUpgradeActive = true;
                    upgradeMenuActive = false;
                }

            }
            else if (70 < angleFromCenter && angleFromCenter <= 110)
            {
                currentMainMenuItem = 2; //Defense Upgrades
                //print("defense upgrades");

            }
        }
        





    }

    void UpgradeSelected()
    {

    }

    void MenuReset()
    {
        upgradeMenu.SetActive(false);
        upgradeMenuOpen = false;

        foreach(GameObject upgrade in attackUpgradeUI)
        {
            upgrade.SetActive(false);
        }

        foreach (GameObject upgrade in defenseUpgradesUI)
        {
            upgrade.SetActive(false);
        }

        foreach (GameObject upgrade in weaponUpgradeUI)
        {
            upgrade.SetActive(false);
        }
    }

    void OpenAttackUpgradeMenu()
    {
        //attackMenuItem.SetActive(true);

        foreach (GameObject upgrade in attackUpgradeUI)
        {
            upgrade.SetActive(true);
        }

        if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f && attackUpgradeActive && !weaponUpgradeActive && !defenseUpgradeActive)
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
            } else
            {
                foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
                {
                    foreach(GameObject obj in board.levelBoards)
                    {
                        obj.SetActive(false);
                    }
                }

                attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].SetActive(false);

                currentAttackMenuItem = -1;
            }

            attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].SetActive(true);


            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && currentAttackMenuItem >= 0)
            {
                if (attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>().upgradeCost <= playerController.playerBullets)
                {
                    attackUpgradeBoardList[currentAttackMenuItem].level++;


                    attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>().AddUpgradeEffect();
                    playerController.playerBullets -= attackUpgradeBoardList[currentAttackMenuItem].levelBoards[attackUpgradeBoardList[currentAttackMenuItem].level].GetComponent<Upgrades>().upgradeCost;
                }
            }
        }



    }

    void OpenDefenseUpgradeMenu()
    {

    }

    void OpenWeaponUpgradeMenu()
    {

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
        //public Color unavailableColor = Color.black;

    }

    [System.Serializable]
    public class AttackUpgradeBoards
    {
        public string name;
        public int level;
        public GameObject[] levelBoards;
        //public bool hasWeapon;
        //public AudioClip recording;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }
}
