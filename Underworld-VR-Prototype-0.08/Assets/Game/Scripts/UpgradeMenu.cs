using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public List<Upgrade> upgradeList = new List<Upgrade>(); //creates a list of menu buttons to access

    public float angleFromCenter; //gets the angle of the finger on the touchpad in relation to the center of the touchpad (0,0)

    public int currentMenuItem; //current menu item
    private int oldMenuItem; //old menu item

    private bool weaponsSelected;

    private Vector2 touchpad;
    public GameObject upgradeMenu;

    public bool upgradeMenuOpen;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OpenUpgradeMenu();
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && upgradeMenuOpen)
        {

        }
		
	}


    void OpenUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
        upgradeMenuOpen = true;
    }

    void UpgradeSelected()
    {

    }

    void MenuRest()
    {
        upgradeMenu.SetActive(false);
        upgradeMenuOpen = false;
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
