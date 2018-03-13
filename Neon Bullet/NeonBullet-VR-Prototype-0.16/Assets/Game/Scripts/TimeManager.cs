using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public float normalTime = 1.0f;
    public float slowDownFactor = 0.00f;
    public float slowDownLength = 2f;

    public bool slowDown = false;
    private bool slowingDown;
    public float timeScale;
    public float previousTimeScale;

    public UpgradeMenu upgradeMenu;
    public WeaponsMenu weaponsMenu;
    public TutorialManager tutorialManager;


    // Use this for initialization
    void Start () {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();

    }

    // Update is called once per frame
    void Update () {
        //slowDown = 
       
        //slowDown = false;

        if (!slowDown)
        {
            Time.timeScale += (1f / slowDownLength) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }

        if (upgradeMenu.upgradeMenuOpen || weaponsMenu.weaponsMenuOpen)
        {
            DoSlowMotion();
        } else
        {
            slowDown = false;
        }


        

        /*if (!slowDown)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.unscaledDeltaTime);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }*/

        //print(Time.timeScale);

        /*if (slowDown)
        {
            SlowMoForDuration();
        }*/


    }

    /*private void FixedUpdate()
    {
        timeScale = Time.timeScale;

        if (previousTimeScale > timeScale)
        {
            slowDown = true;

        }
        else
        {
            slowDown = false;
        }

        previousTimeScale = timeScale;
    }

    /*public void SlowMoDuration(float duration) // public variable that can set the duration of the slow mo
    {
        slowDown = true;
        slowDownLength = duration;
    }*/

    private void SlowMoForDuration() // private function that calls the slowmotion for the set duration
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        slowDown = true;
       
        /*slowDownLength -= Time.unscaledDeltaTime;
        if (slowDownLength <= 0)
        {
            slowDown = false;
            //slowDownLength = 2f;
        }*/
    }

    public void DoSlowMotion()
    {
        //Time.timeScale = Mathf.Lerp(Time.timeScale, slowDownFactor, Time.unscaledDeltaTime);
        //if (!slowDown && !slowingDown)
        {
            Time.timeScale = slowDownFactor;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            //slowDown = true;
            slowDown = true;
        }


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
