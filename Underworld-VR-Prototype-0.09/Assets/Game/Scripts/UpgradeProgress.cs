using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgress : MonoBehaviour {

    public UpgradeMenu upgradeMenu;
    public Image image;

	// Use this for initialization
	void Start () {
        //image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        image.fillAmount = 1- (upgradeMenu.upgradeTimerCounter / upgradeMenu.upgradeTimer);

	}
}
