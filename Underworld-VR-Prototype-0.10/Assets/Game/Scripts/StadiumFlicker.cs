using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumFlicker : MonoBehaviour {

    public float flickerFrequency; // The frequency of the flickering;
    public float flickerTimer;
    public float flickerDuration; //How long each traingle in the stadium shall remain disabled
    public float flickerDurationTimer;
    public GameObject[] triangleStadiums;
    public bool flickerActive;
    public int triangleStadiumCount;

    public bool shouldFlicker;

    public bool alwaysFlicker;

	// Use this for initialization
	void Start () {
        //flickerTimer = flickerFrequency;
        flickerDurationTimer = flickerDuration;
	}
	
	// Update is called once per frame
	void Update () {
        if (shouldFlicker)
        {
            flickerTimer -= Time.deltaTime;
            if (flickerTimer <= 0)// && !flickerActive)
            {
                //flickerActive = true;
                flickerTimer = 0;
                flickerDurationTimer -= Time.deltaTime;
                if (flickerDurationTimer > 0)
                {
                    triangleStadiums[triangleStadiumCount].SetActive(false);
                }
                else
                {
                    triangleStadiums[triangleStadiumCount].SetActive(true);
                    triangleStadiumCount++;

                    if (triangleStadiumCount == (triangleStadiums.Length) )
                    {
                        shouldFlicker = false;
                        flickerTimer = flickerFrequency;
                        triangleStadiumCount = 0;

                        if (alwaysFlicker)
                        {
                            shouldFlicker = true;
                        }


                    }
                    else
                    {
                        //flickerActive = false;
                        flickerDurationTimer = flickerDuration;
                    }
                }
            }
        }
        

	}
}
