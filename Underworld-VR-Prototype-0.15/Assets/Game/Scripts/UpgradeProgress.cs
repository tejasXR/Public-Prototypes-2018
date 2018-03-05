using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeProgress : MonoBehaviour {

    public SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device device;

    public UpgradeMenu upgradeMenu;
    public GameObject dial;
    private Renderer rend;

    public Vector3 scaleOriginal;
    public Vector3 scaleCurrent;

    public float upgradePercent;
    public float textureStrength;
    public Color c;

    public bool hapticProgress;
    public bool hapticEnd;

    public GameObject imageObj;
    private Image image;

	// Use this for initialization
	void Start () {
        image = imageObj.GetComponent<Image>();
        scaleOriginal = dial.transform.localScale;
        rend = dial.GetComponent<Renderer>();
	}

    private void OnEnable()
    {
        scaleCurrent = Vector3.zero;
        textureStrength = 1;
        image.fillAmount = 1;
    }

    // Update is called once per frame
    void Update () {

        device = SteamVR_Controller.Input((int)trackedObj.index); //associates a device with the tracked object;

        if (!upgradeMenu.upgradeDone && upgradeMenu.upgradeSelected)
        {
            upgradePercent = 1- (upgradeMenu.upgradeTimerCounter / upgradeMenu.upgradeTimer);
            //scaleCurrent = scaleOriginal * upgradePercent;
            //dial.transform.localScale = Vector3.Lerp(dial.transform.localScale, scaleCurrent, Time.unscaledDeltaTime * 10f);
            image.fillAmount = upgradePercent;
            //imageAlpha = 1;

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
            textureStrength -= Time.deltaTime;
            if (textureStrength <= 0)
            {
                textureStrength = 1;
            }

            dial.transform.localScale = Vector3.Lerp(dial.transform.localScale, Vector3.zero, Time.unscaledDeltaTime * 4f);

            hapticProgress = false;

            if (!hapticEnd)
            {
                StartCoroutine(EndVibration(.5f, 1500));
                hapticEnd = true;
            }

        }

        rend.material.SetFloat("_MKGlowTexStrength", textureStrength);

        //c.a = textureStrength;
        //image.color = c;


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
