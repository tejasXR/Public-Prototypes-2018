using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsMenu : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;

    private Vector2 touchpad;
    public GameObject weaponsMenu;

    public GameObject[] weapons;
    

    // Use this for initialization
    void Start () {
        weaponsMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;
        touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

        if (device.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            OpenWeaponsMenu();
        }

    }

    void OpenWeaponsMenu()
    {
        weaponsMenu.SetActive(true);

        if (touchpad.x > .5)
        {
            weapons[0].GetComponent<Image>().color = Color.yellow;
        }
    }


}
