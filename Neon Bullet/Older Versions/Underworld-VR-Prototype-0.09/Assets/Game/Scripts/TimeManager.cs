using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float normalTime = 1.0f;
    public float slowDownFactor = 0.00f;
    public float slowDownLength = 2f;

    public bool slowDown = false;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //slowDown = 
        Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);


        /*if (!slowDown)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.unscaledDeltaTime);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }*/

        //print(Time.timeScale);
       

    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

        //slowDown = true;
        /*if (slowDown)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, slowDownFactor, Time.unscaledDeltaTime);
            Time.fixedDeltaTime = Time.timeScale * .02f;
            slowDownLength -= Time.unscaledDeltaTime;
            if (slowDownLength <= 0)
            {
                
                slowDown = false;
                slowDownLength = 2f;
            }
        }*/

        
    }
}
