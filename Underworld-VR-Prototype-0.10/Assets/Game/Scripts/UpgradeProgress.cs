using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgress : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public UpgradeMenu upgradeMenu;
    public Image image;

    public float upgradePercent;
    public float imageAlpha;
    public Color c;

    public bool hapticProgress;
    public bool hapticEnd;

	// Use this for initialization
	void Start () {
        //image = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;

        if (!upgradeMenu.upgradeDone && upgradeMenu.upgradeSelected)
        {
            upgradePercent = 1 - (upgradeMenu.upgradeTimerCounter / upgradeMenu.upgradeTimer);
            image.fillAmount = upgradePercent;
            imageAlpha = 1;

            if (!hapticProgress)
            {
                StartCoroutine(UpgradeProgressHaptic());
                hapticProgress = true;
            }

            hapticEnd = false;

        }

        if (!upgradeMenu.upgradeSelected && upgradeMenu.upgradeDone)
        {
            image.fillAmount = 1;
            imageAlpha += Time.deltaTime;
            if (imageAlpha >= 1)
            {
                imageAlpha = 0;
            }

            hapticProgress = false;

            if (!hapticEnd)
            {
                StartCoroutine(EndVibration(.5f, 1500));
                hapticEnd = true;
            }

        }

        c.a = imageAlpha;
        image.color = c;


    }

    IEnumerator UpgradeProgressHaptic()
    {
        for (int i = 0; i < 10; i++)
        {
            device.TriggerHapticPulse(1500);
            yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .025f); // 5 %
            device.TriggerHapticPulse(1500);
            yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .075f); // 40 %

        }

        /*device.TriggerHapticPulse(1500);
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .05f); // 5 %
        device.TriggerHapticPulse(1500);
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .35f); // 40 %


        device.TriggerHapticPulse(1500); 
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .05f); // 45 % 
        device.TriggerHapticPulse(1500); 
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .2f); // 60 %


        device.TriggerHapticPulse(1500); 
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .05f); // 65 % 
        device.TriggerHapticPulse(1500);
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .15f); // 80 %


        device.TriggerHapticPulse(1500); 
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .025f); // 85 % 
        device.TriggerHapticPulse(1500); 
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .1f); // 95 %

        
        device.TriggerHapticPulse(1500); 
        yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .025f); // 100 % 
        device.TriggerHapticPulse(1500); 
        //yield return new WaitForSeconds(upgradeMenu.upgradeTimer * .03f); // 100 %
        */

    }

    IEnumerator EndVibration(float length, ushort strength)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            device.TriggerHapticPulse(strength);
            strength = (ushort)Mathf.Lerp(strength, 0, Time.deltaTime * 5);
            yield return null; //every single frame for the duration of "length" you will vibrate at "strength" amount
        }
    }
}
