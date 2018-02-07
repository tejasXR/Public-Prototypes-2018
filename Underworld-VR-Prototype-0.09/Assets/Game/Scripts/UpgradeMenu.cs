using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour
{
    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    private Player playerController;

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

    // Use this for initialization
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        MenuReset();

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

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && upgradeMenuOpen && !upgradeSelected && !attackUpgradeActive && !defenseUpgradeActive && !weaponUnlockActive)
        {
            upgradeMenuActive = true;
        }
    }


    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);

        if (upgradeMenuActive)
        {
            cursor.transform.localPosition = touchpad * .125f;
        }
        else if (attackUpgradeActive)
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

        if (upgradeMenuActive && !attackUpgradeActive && !defenseUpgradeActive && !weaponUnlockActive)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                // OPEN WEAPON UNLOCK MENU
                if (340 < angleFromCenter || angleFromCenter <= 20)
                {
                    currentMainMenuItem = 0;
                }
                // OPEN ATTACK UPGRADES MENU
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
                // OPEN DEFENSE UPGRADES MENU
                else if (70 < angleFromCenter && angleFromCenter <= 110)
                {
                    currentMainMenuItem = 2;
                }
            }
            // CLOSE UPGRADE MAIN MENU
            else
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
            mainMenuUIList[oldMainMenuItem].sceneImage.color = mainMenuUIList[oldMainMenuItem].normalColor;
            oldMainMenuItem = currentMainMenuItem;
            mainMenuUIList[currentMainMenuItem].sceneImage.color = mainMenuUIList[currentMainMenuItem].highlightColor;

        }
    }

    void OpenWeaponUnlockMenu()
    {
        if (weaponUnlockActive && !attackUpgradeActive && !defenseUpgradeActive && !upgradeSelected)
        {
            if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
            {
                // FIRE RATE UPGRADES
                if (259 < angleFromCenter || angleFromCenter <= 281)
                {
                    currentWeaponUnlockItem = 0;
                }
                // DAMAGE UPGRADES
                else if (304 < angleFromCenter && angleFromCenter <= 326)
                {
                    currentAttackUpgradeItem = 1;
                }
                // ACCURACY UPGRADES
                else if (349 < angleFromCenter && angleFromCenter <= 11)
                {
                    currentAttackUpgradeItem = 2;
                }
                // BULLET CAPACITY UPGRADES
                else if (34 < angleFromCenter && angleFromCenter <= 56)
                {
                    currentAttackUpgradeItem = 3;
                }
                // BULLETS PER KILL UPGRADES
                else if (79 < angleFromCenter && angleFromCenter <= 101)
                {
                    currentAttackUpgradeItem = 4;
                }
                else
                {
                    foreach (WeaponUnlockBoards weapon in weaponUnlockBoardList)
                    {
                        
                        {
                            weapon.weapon.SetActive(false);
                        }
                    }

                    currentAttackUpgradeItem = -1;
                }
            }
            // BACK BUTTON TO MAIN MENU
            else
            {
                currentWeaponUnlockItem = 5; // Back button is highlighted
            }
        }

        if (currentAttackUpgradeItem != oldAttackUpgradeItem && currentAttackUpgradeItem >= 0)
        {
            attackUpgradeUIList[oldAttackUpgradeItem].sceneImage.color = attackUpgradeUIList[oldAttackUpgradeItem].normalColor;
            oldAttackUpgradeItem = currentAttackUpgradeItem;
            attackUpgradeUIList[currentAttackUpgradeItem].sceneImage.color = attackUpgradeUIList[currentAttackUpgradeItem].highlightColor;
        }

        if (currentAttackUpgradeItem < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].normalColor;
            }
        }

        if (currentAttackUpgradeItem >= 0 && currentAttackUpgradeItem < 5)
        {
            attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards[attackUpgradeBoardList[currentAttackUpgradeItem].level].SetActive(true);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && attackUpgradeActive && !weaponUnlockActive && !upgradeMenuActive && !defenseUpgradeActive && !upgradeSelected)
        {
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
                    foreach (AttackUpgradeBoards board in attackUpgradeBoardList)
                    {
                        foreach (GameObject obj in board.levelBoards)
                        {
                            obj.SetActive(false);
                        }
                    }

                    currentAttackUpgradeItem = -1;
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
            attackUpgradeUIList[oldAttackUpgradeItem].sceneImage.color = attackUpgradeUIList[oldAttackUpgradeItem].normalColor;
            oldAttackUpgradeItem = currentAttackUpgradeItem;
            attackUpgradeUIList[currentAttackUpgradeItem].sceneImage.color = attackUpgradeUIList[currentAttackUpgradeItem].highlightColor;
        }

        if (currentAttackUpgradeItem < 0)
        {
            for (int i = 0; i < 5; i++)
            {
                attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].normalColor;
            }
        }

        if (currentAttackUpgradeItem >= 0 && currentAttackUpgradeItem < 5)
        {
            attackUpgradeBoardList[currentAttackUpgradeItem].levelBoards[attackUpgradeBoardList[currentAttackUpgradeItem].level].SetActive(true);
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && attackUpgradeActive && !weaponUnlockActive && !upgradeMenuActive && !defenseUpgradeActive && !upgradeSelected)
        {
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
            }
        }
    }

    void OpenDefenseUpgradeMenu()
    {

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
            weaponUnlockUIList[i].sceneImage.color = weaponUnlockUIList[i].unavailableColor;
        }

        for (int i = 0; i < 5; i++)
        {
            attackUpgradeUIList[i].sceneImage.color = attackUpgradeUIList[i].unavailableColor;
        }

        for (int i = 0; i < 5; i++)
        {
            defenseUpgradeUIList[i].sceneImage.color = defenseUpgradeUIList[i].unavailableColor;
        }

        // Make all extending description boards unavailable
        foreach (WeaponUnlockBoards weapon in weaponUnlockBoardList)
        {
            
            {
                weapon.weapon.SetActive(false);
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
    public class WeaponUnlockUI
    {
        public string name;
        public bool hasWeapon;
        public Image sceneImage;
        public Color normalColor = Color.white;
        public Color highlightColor = Color.grey;
        public Color pressedColor = Color.yellow;
        public Color unavailableColor = Color.black;

    }

    [System.Serializable]
    public class WeaponUnlockBoards
    {
        public string name;
        //public int level;
        public GameObject weapon;
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
    }

    [System.Serializable]
    public class DefenseUpgradeUI
    {
        public string name;
        public Image sceneImage;
        public Color normalColor = Color.white;
        public Color highlightColor = Color.grey;
        public Color pressedColor = Color.yellow;
        public Color unavailableColor = Color.black;
    }

    [System.Serializable]
    public class DefenseUpgradeBoards
    {
        public string name;
        public int level;
        public GameObject[] levelBoards;
    }
}