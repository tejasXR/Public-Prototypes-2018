using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public List<Weapon> weaponList = new List<Weapon>(); //creates a list of menu buttons to access

    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    //public AudioSource audio;
    //public AudioClip[] clips;

    public int currentMenuItem; //current menu item
    private int oldMenuItem; //old menu item

    private Vector2 touchpad;
    public GameObject weaponsMenu;

    public bool weaponsMenuOpen;
    private bool weaponsSelected;

    public GameObject cursor;

    public GameObject[] weapons; //Here are the weapon gameObject that we will show or hide depending on what weapon the user picks in the weapons menu

    public Material[] menuMat;

    private WeaponActive weaponActive; // Adding weapon active to set the current active weapon

    public bool weaponSelected;

    public bool firstPressUp;

    private GameManager gameManager;

    public GameObject blurredProjection;
    public AudioSource itemHoverSound;

    private TimeManager timeManager;


    // Use this for initialization
    void Start () {
        weaponsMenu.SetActive(false);
        timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>();
        weaponActive = GetComponent<WeaponActive>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        CheckWeapons();

        for (int i = 0; i < 5; i++)
        {
            if (weaponList[i].hasWeapon)
            {
                weaponList[i].sphere.GetComponent<Renderer>().material = menuMat[0];
                weaponList[i].icon.SetActive(true);
            } else
            {
                weaponList[i].sphere.GetComponent<Renderer>().material = menuMat[1];
                weaponList[i].icon.SetActive(false);
            }
        }



        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        /*for (int i = 0; i <= 4; i++)
        {
            if (weaponList[i].hasWeapon)
            {
                weaponList[i].sphere.GetComponent<Renderer>.material = menuMat[0]; // set all weapons that you have as available but inactive
                //weaponList[i].sceneImage.color = weaponList[i].normalColor;
            }
            /*else
            {
                weaponList[i].weaponObj.SetActive(false);
                //weaponList[i].sceneImage.color = weaponList[i].unavailableColor;
            }*/
        

        currentMenuItem = 0;
        oldMenuItem = 1;
    }
	
	// Update is called once per frame
	void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;

        //touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && !weaponsMenuOpen)// && !gameManager.inRedemption)
        {
            StartCoroutine(ButtonPressHaptics(1000));
            weaponActive.DisableAllWeapons();
            //timeManager.DoSlowMotion();
            //blurredProjection.SetActive(true);
            CheckWeapons(); //check what weapons we have unlocked so we can show this accordingly via UI

            weaponsMenuOpen = true;

        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) && weaponsMenuOpen)
        {
            firstPressUp = true;
        }

        /*if (weaponsMenuOpen && device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            WeaponSelected();
        }*/

        if (weaponsMenuOpen)
        {
            OpenWeaponsMenu();
        }




    }

    void OpenWeaponsMenu()
    {
        //blurredProjection.SetActive(true);
        weaponsMenuOpen = true;
        weaponsMenu.SetActive(true);

        touchpad.x = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x;
        touchpad.y = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y;

        Vector2 fromVector2 = new Vector2(0, 1);
        Vector2 toVector2 = touchpad;

        cursor.transform.localPosition = Vector3.Lerp(cursor.transform.localPosition, touchpad * .1f, Time.unscaledDeltaTime * 10f);

        angleFromCenter = Vector2.Angle(fromVector2, toVector2);
        Vector3 cross = Vector3.Cross(fromVector2, toVector2);

        if (cross.z > 0)
        {
            angleFromCenter = 360 - angleFromCenter;
        }

        if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
        {
            // PISTOL
            if (259 < angleFromCenter && angleFromCenter <= 281)
            {
                currentMenuItem = 0;
            }
            // RIFLE
            else if (304 < angleFromCenter && angleFromCenter <= 326)
            {
                currentMenuItem = 1;
            }
            // SHOTGUN
            else if (349 < angleFromCenter || angleFromCenter <= 11)
            {
                currentMenuItem = 2;
            }
            // SABER SWORD
            else if (34 < angleFromCenter && angleFromCenter <= 56)
            {
                currentMenuItem = 3;
            }
            // HYPER RIFLE
            else if (79 < angleFromCenter && angleFromCenter <= 101)
            {
                currentMenuItem = 4;
            } else
            {
                

                //currentMenuItem = -1;
            }
        } // BACK BUTTON TO MAIN MENU
        else
        {
            /*foreach (GameObject weapon in weapons)
            {
                weapon.SetActive(false);
            }*/

            if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && weaponsMenuOpen && firstPressUp)
            {
                MenuReset();
            }

            currentMenuItem = 5; // Back button is highlighted
        }

        //If we have the tape for the selected menu item
        if (currentMenuItem >= 0)
        {
            if (currentMenuItem != oldMenuItem && weaponList[currentMenuItem].hasWeapon)
            {
                itemHoverSound.Play();
                StartCoroutine(ButtonPressHaptics(300));

                if (oldMenuItem < 5)
                {
                    weapons[oldMenuItem].SetActive(false);

                }

                weaponList[oldMenuItem].sphere.GetComponent<Renderer>().material = menuMat[0];
                //weaponList[oldMenuItem].sceneImage.color = weaponList[oldMenuItem].normalColor;
                oldMenuItem = currentMenuItem;
                //weapons[currentMenuItem].SetActive(true);
                //weaponList[currentMenuItem].sceneImage.color = weaponList[currentMenuItem].highlightColor;
                //print("changing color");

                //if (currentMenuItem >= 0 && )


                if (currentMenuItem == 5)
                {
                    weaponList[currentMenuItem].sphere.GetComponent<Renderer>().material = menuMat[2];
                }
                else
                {
                    weaponList[currentMenuItem].sphere.GetComponent<Renderer>().material = weaponList[currentMenuItem].highlightMat;
                }

            }
        }

        if (currentMenuItem >= 0 && currentMenuItem < 5 && weaponList[currentMenuItem].hasWeapon)
        {
            weapons[currentMenuItem].SetActive(true);
        }/* else
        {
            foreach (GameObject weapon in weapons)
            {
                weapon.SetActive(false);
            }
        }*/


        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && weaponsMenuOpen && firstPressUp)
        {
            if (currentMenuItem >= 0 && currentMenuItem < 5)
            {
                StartCoroutine(ButtonPressHaptics(1000));
                WeaponSelected();
                MenuReset();
            }
        }
    }


    void WeaponSelected()
    {
        //weaponsSelected = true;

        // Linking to WeaponActive script for better managing of the current active weapon
        weaponSelected = true;

        switch(currentMenuItem)
        {
            case 0:
                weaponActive.WeaponToActivate("PISTOL");
                break;
            case 1:
                weaponActive.WeaponToActivate("RIFLE");
                break;
            case 2:
                weaponActive.WeaponToActivate("SHOTGUN");
                break;
            case 3:
                weaponActive.WeaponToActivate("SABER SWORD");
                break;
            case 4:
                weaponActive.WeaponToActivate("HYPER RIFLE");
                break;
        }
        
        /*
        for (int i = 0; i <= 4; i++)
        {
            weapons[i].SetActive(false);
        }

        weapons[currentMenuItem].SetActive(true);
        */

    }

    private void MenuReset()
    {
        firstPressUp = false;
        weaponsMenuOpen = false;
        weaponSelected = false;
        weaponsSelected = false;
        

        /*foreach (Weapon weapon in weaponList)
        {
            weapon.sphere.GetComponent<Renderer>().material = menuMat[1]; // set all icons to unavailable
            weapon.icon.SetActive(false); // set all icons to unavailable


            if (weapon.hasWeapon)
            {
                weapon.sphere.GetComponent<Renderer>().material = menuMat[0]; // set all weapons that you have as available but inactive
                weapon.icon.SetActive(true);
            }
        }*/

        for (int i = 0; i < 5; i++)
        {
            if (weaponList[i].hasWeapon)
            {
                weaponList[i].sphere.GetComponent<Renderer>().material = menuMat[0];
                weaponList[i].icon.SetActive(true);
            }
            else
            {
                weaponList[i].sphere.GetComponent<Renderer>().material = menuMat[1];
                weaponList[i].icon.SetActive(false);
            }
        }

        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        weaponsMenu.SetActive(false);

        weaponActive.WeaponToActivate(weaponActive.currentWeapon);
        blurredProjection.SetActive(false);


        /*
        //Reset colors for the buttons
        foreach (Weapon weapon in weaponList)
        {
            weapon.sphere.GetComponent<Renderer>().material = inactiveMat;
            //weapon.sceneImage.color = weapon.normalColor;
        }

        for (int i = 0; i <= 4; i++)
        {
            if (weaponList[i].hasWeapon)
            {
                weaponList[i].weaponObj.SetActive(true);
                //weaponList[i].sceneImage.color = weaponList[i].normalColor;
            }
            else
            {
                weaponList[i].weaponObj.SetActive(false);
                //weaponList[i].sceneImage.color = weaponList[i].unavailableColor;
            }
        }*/

        currentMenuItem = 0;
        oldMenuItem = 0;
    }

    void CheckWeapons()
    {
        if (weaponActive.unlockPistol)
        {
            weaponList[0].hasWeapon = true;
        }

        if (weaponActive.unlockRifle)
        {
            weaponList[1].hasWeapon = true;
        }

        if (weaponActive.unlockShotgun)
        {
            weaponList[2].hasWeapon = true;
        }

        if (weaponActive.unlockSaberSword)
        {
            weaponList[3].hasWeapon = true;
        }

        if (weaponActive.unlockHyperRifle)
        {
            weaponList[4].hasWeapon = true;
        }
    }

    IEnumerator ButtonPressHaptics(float strength)
    {
        device.TriggerHapticPulse((ushort)strength);
        yield return new WaitForSeconds(.1f);

    }

    [System.Serializable]
    public class Weapon
    {
        public string name;
        public bool hasWeapon;
        public GameObject icon;
        //public AudioClip recording;
        public GameObject sphere;
        public Material highlightMat;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }
}
