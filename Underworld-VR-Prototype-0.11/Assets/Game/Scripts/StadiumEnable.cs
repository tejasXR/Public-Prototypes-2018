using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumEnable : MonoBehaviour {

    public float scaleCurrent;
    public float scaleOriginal;

    public bool flash;
    public bool scaling;
    public bool scalingDone;

    public GameObject abovePlatform;

    public bool isStadium;
    public bool isPlatformTriangles;
    public bool isRedemptionTriangles;

    public float delay;
    public float delayCounter;

    public GameObject[] platformTriangles;
    //public AudioSource[] platformSoundOn;
    //public AudioSource[] platformSoundNormal;

    // Use this for initialization
    void Start() {
        scaleOriginal = abovePlatform.transform.localScale.y;
    }

    private void OnEnable()
    {
        /*foreach (AudioSource sound in platformSoundOn)
        {
            sound.enabled = false;
        }*/

        delayCounter = delay;

        if (isStadium)
        {
            scaleCurrent = .4f;
            StartCoroutine(StadiumFlash());
        }

        if (isPlatformTriangles)
        {
            scaleCurrent = 0;
            StartCoroutine(PlatformTraingleFlash());
        }

        if (isRedemptionTriangles)
        {
            foreach (GameObject platformTriangle in platformTriangles)
            {
                platformTriangle.SetActive(false);
            }

            scaleCurrent = 0;
            StartCoroutine(PlatformTraingleFlash());
        }
    }

    // Update is called once per frame
    void Update() {

        if (isPlatformTriangles || isRedemptionTriangles)
        {
            if (delayCounter >= 0)
            {
                delayCounter -= Time.deltaTime;
            }
            else
            {
                delayCounter = 0;
                scaling = true;
            }

            if (scaling && !scalingDone)
            {
               
                foreach (GameObject platformTriangle in platformTriangles)
                {
                    platformTriangle.SetActive(true);
                }
                
                scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal, Time.deltaTime);
                abovePlatform.transform.localScale = new Vector3(abovePlatform.transform.localScale.x, scaleCurrent, abovePlatform.transform.localScale.z);
            }

            if ((scaleCurrent / scaleOriginal) > .98f)
            {
                scalingDone = true;
            }
        }

        if (isPlatformTriangles)
        {
            if (delayCounter >= 0)
            {
                delayCounter -= Time.deltaTime;
            }
            else
            {
                delayCounter = 0;
                scaling = true;
            }

            if (scaling && !scalingDone)
            {

                foreach (GameObject platformTriangle in platformTriangles)
                {
                    platformTriangle.SetActive(true);
                }

                scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal, Time.deltaTime);
                abovePlatform.transform.localScale = new Vector3(abovePlatform.transform.localScale.x, scaleCurrent, abovePlatform.transform.localScale.z);
            }

            if ((scaleCurrent / scaleOriginal) > .98f)
            {
                scalingDone = true;
            }
        }

        /*foreach(AudioSource audio in platformSoundNormal)
        {
            audio.pitch = .6f * Time.timeScale;
        }*/

    }

    IEnumerator StadiumFlash()
    {
        yield return new WaitForSeconds(delay);
        /*abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.25f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.25f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.1f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.1f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.1f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.1f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.1f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.1f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.01f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.01f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.01f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.01f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.005f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.005f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.005f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.005f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.0005f);
        abovePlatform.SetActive(false);
        yield return new WaitForSeconds(.0005f);
        abovePlatform.SetActive(true);
        yield return new WaitForSeconds(.0005f);*/

        abovePlatform.GetComponent<StadiumFlicker>().shouldFlicker = true;
    }

    IEnumerator PlatformTraingleFlash()
    {
        yield return new WaitForSeconds(delay);

        //for (int i = 0; i < 4; i++)
        {
          //  platformTriangles[i].SetActive(false);
        }

        /*platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.5f);
        platformTriangles[1].SetActive(true);
        yield return new WaitForSeconds(.5f);
        platformTriangles[2].SetActive(true);
        yield return new WaitForSeconds(.5f);
        */
        /*foreach(AudioSource sound in platformSoundOn)
        {
            sound.enabled = true;
        }*/

        yield return new WaitForSeconds(1f);



        platformTriangles[1].SetActive(false);
        yield return new WaitForSeconds(.05f);
        platformTriangles[1].SetActive(true);
        
        yield return new WaitForSeconds(.05f);

        platformTriangles[3].SetActive(false);
        yield return new WaitForSeconds(.03f);

        platformTriangles[2].SetActive(false);
        yield return new WaitForSeconds(.02f);

        platformTriangles[1].SetActive(false);
        yield return new WaitForSeconds(.05f);

        platformTriangles[3].SetActive(true);
        yield return new WaitForSeconds(.03f);

        platformTriangles[1].SetActive(true);
        yield return new WaitForSeconds(.05f);

       


        platformTriangles[2].SetActive(true);
        yield return new WaitForSeconds(.02f);
        platformTriangles[2].SetActive(false);
        yield return new WaitForSeconds(.01f);
        platformTriangles[2].SetActive(true);
        yield return new WaitForSeconds(.01f);

        platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.05f);
        platformTriangles[0].SetActive(false);
        yield return new WaitForSeconds(.05f);
        platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.05f);
        platformTriangles[0].SetActive(false);
        yield return new WaitForSeconds(.05f);
        platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.01f);
        platformTriangles[0].SetActive(false);
        yield return new WaitForSeconds(.01f);
        platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.01f);
        platformTriangles[0].SetActive(false);
        yield return new WaitForSeconds(.01f);
        platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.005f);
        platformTriangles[0].SetActive(false);
        yield return new WaitForSeconds(.005f);
        platformTriangles[0].SetActive(true);
        yield return new WaitForSeconds(.005f);

        platformTriangles[3].SetActive(false);
        yield return new WaitForSeconds(.02f);
        platformTriangles[3].SetActive(true);
        yield return new WaitForSeconds(.02f);
        platformTriangles[3].SetActive(false);
        yield return new WaitForSeconds(.01f);
        platformTriangles[3].SetActive(true);
        yield return new WaitForSeconds(.01f);
    }
}
