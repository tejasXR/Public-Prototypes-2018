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
    public float lightIntensitySmooth;

    public float delay;
    public float delayCounter;

    private void Awake()
    {
        light = GetComponent<Light>();

        lightIntensity = lightIntensityMax;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {
        
	}

    private void OnEnable()
    {
        delayCounter = delay;
        light.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.redemptionActive)
        {
            delayCounter -= Time.deltaTime;
            if (delayCounter <= 0)
            {
                lightOn = true;
            }
        }

        if (lightOn)
        {
            /*
            lightIntensity += Time.deltaTime;
            if (lightIntensity >= lightIntensityMin)
            {
                lightIntensity = lightIntensityMin;
            }

            lightIntensitySmooth = Mathf.Lerp(lightIntensitySmooth, lightIntensity, Time.deltaTime);
            light.intensity = lightIntensitySmooth;
            */

            //lightIntensity = Mathf.Lerp(lightIntensity, lightIntensityMax, Time.deltaTime * .25f);
            light.intensity = Mathf.Lerp(light.intensity, lightIntensityMax, Time.deltaTime * .25f);
        }

        
    }
}
