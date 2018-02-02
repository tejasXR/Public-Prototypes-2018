using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedemptionLightScript : MonoBehaviour {

    private GameManager gameManager;

    public bool lightOn;
    private Light light;

    public float lightIntensityMax;
    public float lightIntensityMin;
    public float lightIntensity;

    public float delay;
    public float delayCounter;

    // Use this for initialization
    void Start () {
        light = GetComponent<Light>();

        lightIntensity = lightIntensityMax;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    private void OnEnable()
    {
        delayCounter = delay;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.redemptionActive)
        {
            delayCounter -= Time.deltaTime;
            if (delayCounter <= 0)
            {
                lightOn = true;
            }
        }

        if (lightOn)
        {
            lightIntensity -= Time.deltaTime;
            if (lightIntensity <= lightIntensityMin)
            {
                lightIntensity = lightIntensityMax;
            }

            light.intensity = lightIntensity;
        }
    }
}
