using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

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
}
