using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLightScript : MonoBehaviour {

    private GameManager gameManager;

    public bool lightOn;
    private Light light;
    public float lightIntensity;
    public float lightIntensityMax;
    //public float lightIntensityTarget;


    public float delay;
    public float delayCounter;

    private void Awake()
    {
        light = GetComponent<Light>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //lightIntensityMax = light.intensity;
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
            //lightIntensity = Mathf.Lerp(lightIntensity, lightIntensityMax, Time.deltaTime * .25f);
            light.intensity = Mathf.Lerp(light.intensity, lightIntensityMax, Time.deltaTime * .25f);

        }

        //light.intensity = lightIntensity;
		
	}
}
