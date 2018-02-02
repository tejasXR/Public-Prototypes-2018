using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLightScript : MonoBehaviour {

    private GameManager gameManager;

    public bool lightOn;
    private Light light;
    public float lightIntensity;
    public float lightIntensityOriginal;

	// Use this for initialization
	void Start () {
        light = GetComponent<Light>();
        light.intensity = lightIntensity;
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (gameManager.playerReachedStadium && !lightOn)
        {
            lightOn = true;
        }

        if (lightOn)
        {
            lightIntensity = Mathf.Lerp(lightIntensity, lightIntensityOriginal, Time.deltaTime * .5f);
        }

        light.intensity = lightIntensity;
		
	}
}
