﻿using System.Collections;
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

    //public GameObject[] weapons; //Here are the weapon gameObject that we will show or hide depending on what weapon the user picks in the weapons menu

    public Material inactiveMat;

    public WeaponActive weaponActive; // Adding weapon active to set the current active weapon
    

    // Use this for initialization
    void Start () {
        weaponsMenu.SetActive(false);
        weaponActive = GetComponent<WeaponActive>();

        foreach (Weapon weapon in weaponList)
        {
            weapon.weaponObj.GetComponent<Renderer>().material = inactiveMat;
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
        }

        currentMenuItem = 0;
        oldMenuItem = 1;
    }
	
	// Update is called once per frame
	void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;

        //touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OpenWeaponsMenu();
        }

        /*if (weaponsMenuOpen && device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            WeaponSelected();
        }*/

        if (weaponsMenuOpen && device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            WeaponSelected();
            MenuReset();
        }

    }

    void OpenWeaponsMenu()
    {
        weaponsMenuOpen = true;
        weaponsMenu.SetActive(true);

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

        if (Mathf.Abs(touchpad.x) > .3f || Mathf.Abs(touchpad.y) > .3f)
        {
            //PISTOL
            if (324 < angleFromCenter || angleFromCenter <= 36)
            {
                currentMenuItem = 0;

            }
            //RIFLE
            else if (36 < angleFromCenter && angleFromCenter <= 108)
            {
                currentMenuItem = 1;
            }
            //SHOTGUN
            else if (108 < angleFromCenter && angleFromCenter <= 180)
            {
                currentMenuItem = 2;
            }
            //SABER SWORD
            else if (180 < angleFromCenter && angleFromCenter <= 252)
            {
                currentMenuItem = 3;
            }
            //HYPER RIFLE
            else if (252 < angleFromCenter && angleFromCenter <= 324)
            {
                currentMenuItem = 4;
            }
        } else
        {
            currentMenuItem = -1;
        }
        

        //To tell when to light up or not

        //If we have the tape for the selected menu item
        if (weaponList[currentMenuItem].hasWeapon && currentMenuItem >= 0)
        {
            if (currentMenuItem != oldMenuItem)
            {
                weaponList[oldMenuItem].weaponObj.GetComponent<Renderer>().material = inactiveMat;
                //weaponList[oldMenuItem].sceneImage.color = weaponList[oldMenuItem].normalColor;
                oldMenuItem = currentMenuItem;
                weaponList[currentMenuItem].weaponObj.GetComponent<Renderer>().material = weaponList[currentMenuItem].highlightMat;
                //weaponList[currentMenuItem].sceneImage.color = weaponList[currentMenuItem].highlightColor;
                //print("changing color");
            }
        }
    }

    void WeaponSelected()
    {
        //weaponsSelected = true;

        // Linking to WeaponActive script for better managing of the current active weapon

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
                weaponActive.WeaponToActivate("LASER RIFLE");
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
        weaponsMenu.SetActive(false);

        //Reset colors for the buttons
        foreach (Weapon weapon in weaponList)
        {
            weapon.sceneImage.color = weapon.normalColor;
        }
        for (int i = 0; i <= 4; i++)
        {
            if (weaponList[i].hasWeapon)
            {
                weaponList[i].sceneImage.color = weaponList[i].normalColor;
            }
            else
            {
                weaponList[i].sceneImage.color = weaponList[i].unavailableColor;
            }
        }

        currentMenuItem = 0;
        oldMenuItem = 0;
    }

    [System.Serializable]
    public class Weapon
    {
        public string name;
        public bool hasWeapon;
        //public AudioClip recording;
        public GameObject weaponObj;
        public Material highlightMat;
        //public Image sceneImage;
        //public Color normalColor = Color.white;
        //public Color highlightColor = Color.grey;
        //public Color pressedColor = Color.yellow;
        //public Color unavailableColor = Color.black;

    }
}
