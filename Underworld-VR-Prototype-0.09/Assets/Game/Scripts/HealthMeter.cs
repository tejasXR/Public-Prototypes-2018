using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour {

    private Player playerController;
    public float healthSmoothPercent;

    public float scaleOriginal;
    public float scaleCurrent;

    public float meterXCurrent;
    //public float meterXOriginal;

	// Use this for initialization
	void Start () {

        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        scaleOriginal = transform.localScale.x;
        //meterXOriginal = transform.localPosition.x;
	}
	
	// Update is called once per frame
	void Update () {

        healthSmoothPercent = Mathf.Lerp(healthSmoothPercent, playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier), Time.deltaTime);

        scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal * healthSmoothPercent, Time.deltaTime);
        meterXCurrent = Mathf.Lerp(meterXCurrent, -.03f + (.037f * healthSmoothPercent), Time.deltaTime);

        transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
        transform.localPosition = new Vector3(meterXCurrent, transform.localPosition.y, transform.localPosition.z);
	}
}
