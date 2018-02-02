using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLightScript : MonoBehaviour {

    private GameManager gameManager;

    public bool lightOn;
    private Light light;
    public float lightIntensity;
    public float lightIntensityOriginal;
    public float delay;
    public float delayCounter;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        lightIntensityOriginal = light.intensity;
        delayCounter = delay;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameManager.playerReachedStadium && !lightOn)
        {
            delayCounter -= Time.deltaTime;
            if (delayCounter <= 0)
            {
                lightOn = true;
            }
        }

        if (lightOn)
        {
            lightIntensity = Mathf.Lerp(lightIntensity, lightIntensityOriginal, Time.deltaTime * .25f);
        }

        light.intensity = lightIntensity;
		
	}
}
