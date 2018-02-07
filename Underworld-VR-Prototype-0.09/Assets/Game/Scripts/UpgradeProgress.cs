using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgress : MonoBehaviour {

    public UpgradeMenu upgradeMenu;
    public Image image;

    public float upgradePercent;
    public float imageAlpha;
    public Color c;

	// Use this for initialization
	void Start () {
        //image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!upgradeMenu.upgradeDone)
        {
            upgradePercent = 1 - (upgradeMenu.upgradeTimerCounter / upgradeMenu.upgradeTimer);
            image.fillAmount = upgradePercent;
            imageAlpha = 1;
        } else
        {
            image.fillAmount = 1;
            imageAlpha += Time.deltaTime;
            if (imageAlpha >= 1)
            {
                imageAlpha = 0;
            }
            
        }

        c.a = imageAlpha;
        image.color = c;


    }
}
