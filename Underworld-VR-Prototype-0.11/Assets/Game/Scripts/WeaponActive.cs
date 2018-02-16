using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActive : MonoBehaviour {

    public GameObject[] weapons; // The list of weapons
    private GameManager gameManager;
    public int weaponInt; // Used for internal counting purposes
    public string currentWeapon;
    public string previousWeapon;
    public bool unlockPistol;
    public bool unlockRifle;
    public bool unlockShotgun;
    public bool unlockSaberSword;
    public bool unlockHyperRifle;
    //public GameObject weaponActive; // The specific weapon that is active in the moment;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        // In the start, set all weapons to in active

        DisableAllWeapons();

        //Debug.Log(CheckCurrentWeapon());
    }
	
	

    public void WeaponToActivate(string weaponName)
    {
        DisableAllWeapons();

        //CheckCurrentWeapon(); // Gets the name of the weapon player has before switching to a new weapon

        switch (weaponName)
        {
            case "PISTOL":
                if (unlockPistol)
                {
                    weapons[0].SetActive(true);
                    weaponInt = 0;
                    //print("PISTOL");
                }
                break;
            case "RIFLE":
                if (unlockRifle)
                {
                    //print("Rifle");
                    weapons[1].SetActive(true);
                    weaponInt = 1;
                }
                break;
            case "SHOTGUN":
                if (unlockShotgun)
                {
                    weapons[2].SetActive(true);
                    weaponInt = 2;
                }
                break;
            case "SABER SWORD":
                if (unlockSaberSword || gameManager.inRedemption)
                {
                    weapons[3].SetActive(true);
                    weaponInt = 3;
                }
                break;
            case "HYPER RIFLE":
                if (unlockHyperRifle)
                {
                    weapons[4].SetActive(true);
                    weaponInt = 4;
                }
                break;
           
        }
        currentWeapon = weaponName;
    }

    public string CheckCurrentWeapon()
    {
        //string weaponName;
        switch(weaponInt)
        {
            case 0:
                previousWeapon = "PISTOL";
                break;
            case 1:
                previousWeapon = "RIFLE";
                break;
            case 2:
                previousWeapon = "SHOTGUN";
                break;
            case 3:
                previousWeapon = "SABER SWORD";
                break;
            case 4:
                previousWeapon = "HYPER RIFLE";
                break;
        }

        return previousWeapon;
    }

    public void DisableAllWeapons()
    {
        CheckCurrentWeapon();

        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
}
