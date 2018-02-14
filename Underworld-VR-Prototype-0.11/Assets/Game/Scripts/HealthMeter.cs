using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthMeter : MonoBehaviour {

    private Player playerController;
    private GameManager gameManager;

    public float healthSmoothPercent;

    public float scaleOriginal;
    public float scaleCurrent;

    public float meterXCurrent;
    public float meterXOriginal;

    public float meterZCurrent;
    public float meterZOriginal;

    public bool isOnSaberSword;


	// Use this for initialization
	void Start () {

        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        scaleOriginal = transform.localScale.x;
        meterXOriginal = transform.localPosition.x;

        if (isOnSaberSword)
        {
            meterZOriginal = transform.localPosition.z;
        }
    }

    private void OnEnable()
    {
        scaleCurrent = 0;
        meterXCurrent = meterXOriginal - scaleOriginal / 2;

        if (isOnSaberSword)
        {
            if (gameManager.inRedemption)
            {
                this.gameObject.SetActive(false);
            } else
            {
                meterZCurrent = meterZOriginal - scaleOriginal / 2;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        healthSmoothPercent = Mathf.Lerp(healthSmoothPercent, playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier), Time.deltaTime * 5f);

        scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal * healthSmoothPercent, Time.deltaTime * 3f);

        if (isOnSaberSword)
        {
            meterZCurrent = Mathf.Lerp(meterZCurrent, (meterZOriginal - scaleOriginal / 2) + ((scaleOriginal / 2) * healthSmoothPercent), Time.deltaTime * 3f);
            transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, meterZCurrent);
        } else
        {
            meterXCurrent = Mathf.Lerp(meterXCurrent, (meterXOriginal - scaleOriginal / 2) + ((scaleOriginal / 2) * healthSmoothPercent), Time.deltaTime * 3f);
            transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
            transform.localPosition = new Vector3(meterXCurrent, transform.localPosition.y, transform.localPosition.z);
        }

        //transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
        //transform.localPosition = new Vector3(meterXCurrent, transform.localPosition.y, transform.localPosition.z);
	}
}
