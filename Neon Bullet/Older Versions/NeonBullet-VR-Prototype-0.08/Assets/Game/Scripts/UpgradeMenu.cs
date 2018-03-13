using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public List<Upgrade> upgradeList = new List<Upgrade>(); //creates a list of menu buttons to access

    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public int currentMainMenuItem; //current menu item for the main menu
    private int oldMainMenuItem; //old menu item for the main menu

    public int currentAttackUpgradeItem;
    public int oldAttackUpgradeItem;

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
    public GameObject[] attackUpgradeBoards;
    //public GameObject attackMenuItem;


    public GameObject[] defenseUpgradesUI;
    public GameObject[] weaponUpgradeUI;


    


    // Use this for initialization
    void Start () {

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
        





        cursor.transform.localPosition = touchpad;
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
            if (349 < angleFromCenter || angleFromCenter <= 11)
            {
                currentAttackUpgradeItem = 0; //Weapon Upgrades
                attackUpgradeBoards[0].SetActive(true);
                //print("attack 1");
            }
            else if (304 < angleFromCenter && angleFromCenter <= 326)
            {
                currentAttackUpgradeItem = 1; //Attack Upgrades
                attackUpgradeBoards[1].SetActive(true);
                //print("attack 2");
            }
            else if (259 < angleFromCenter && angleFromCenter <= 281)
            {
                currentAttackUpgradeItem = 2; //Attack Upgrades
                attackUpgradeBoards[2].SetActive(true);
                //print("attack 3");
            }
            else if (214 < angleFromCenter && angleFromCenter <= 236)
            {
                currentAttackUpgradeItem = 3; //Defense Upgrades
                attackUpgradeBoards[3].SetActive(true);
                //print("attack 4");

            }
            else if (169 < angleFromCenter && angleFromCenter <= 191)
            {
                currentAttackUpgradeItem = 4; //Defense Upgrades
                attackUpgradeBoards[4].SetActive(true);
                //print("attack 5");
            } else
            {
                foreach (GameObject board in attackUpgradeBoards)
                {
                    board.SetActive(false);
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
    public class Upgrade
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
}
