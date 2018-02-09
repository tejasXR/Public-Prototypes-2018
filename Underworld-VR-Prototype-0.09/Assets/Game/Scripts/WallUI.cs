using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallUI : MonoBehaviour {

    private GameManager gameManager;

    public Transform UIStartPosition;
    public Transform UITargetPosition;
    public Transform timerStartTransform;
    public Transform timerTargetTransform;
    public GameObject timerObj;
    public TextMeshPro killCountText;

    public TextMeshPro[] textAlphaControl;

    public GameObject wallUIWhole;

    public float enemiesLeft; // A smooth counter that lerps to the enemiesToSpawn var from the gameManager;

    public float moveSpeed;
    public float alpha;
    public float bufferTime; // buffer time before the timer ui drops down

    public bool isRoundUI;
    public bool isRedemptionUI;

    private void Awake()
    {
        if (isRoundUI)
        {
            alpha = 0;
        }
        if (isRedemptionUI)
        {
            alpha = 1;
        }

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // Use this for initialization
    void Start () {

        transform.position = UIStartPosition.position;
	}

    private void OnEnable()
    {
        if (isRoundUI)
        {
            alpha = 0;
            //CheckRoundText();
        }
        if (isRedemptionUI)
        {
            alpha = 1;
        }

        transform.position = UIStartPosition.position;
        //timerObj.transform.position = timerStartTransform.position;

        

    }

    // Update is called once per frame
    void Update () {

       
        if (gameManager.roundActive || gameManager.redemptionActive)
        {
            if (isRoundUI)
            {
                alpha = Mathf.SmoothStep(alpha, 1, Time.deltaTime * 5f);
            }

            if (isRedemptionUI)
            {
                alpha = Mathf.SmoothStep(alpha, (gameManager.redemptionMeter / gameManager.redemptionMeterMax) - .1f, Time.deltaTime * 5f);
            }

            transform.position = Vector3.Lerp(transform.position, UITargetPosition.position, Time.deltaTime * moveSpeed);

            if (isRedemptionUI)
            {
                bufferTime -= Time.deltaTime;
                if (bufferTime <= 0)
                {
                    timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerTargetTransform.position, Time.deltaTime);
                    bufferTime = 0;
                }
            }

            
        }
        else //automatically fade out if the player is not in a round
        {
            if (isRoundUI)
            {
                //timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerStartTransform.position, Time.deltaTime * 1.5f);
                alpha = Mathf.SmoothStep(alpha, 0, Time.deltaTime * 7f);

                if (alpha < .01)
                {
                    alpha = 0;
                    wallUIWhole.SetActive(false);
                }
            }
            
        }

        if ((isRoundUI && gameManager.redemptionActive) || (isRedemptionUI && gameManager.roundActive))
        {
            wallUIWhole.SetActive(false);
        }

        foreach (TextMeshPro text in textAlphaControl)
        {
            text.alpha = alpha;
        }

        enemiesLeft = Mathf.Lerp(enemiesLeft, (gameManager.enemiesToSpawn - gameManager.enemiesDestroyed), Time.deltaTime * 5f);

        killCountText.text = Mathf.RoundToInt(enemiesLeft).ToString();

    }

    void CheckRoundText()
    {
        switch (gameManager.roundCurrent)
        {
            case 1:
                killCountText.text = "ROUND ONE";
                break;
            case 2:
                killCountText.text = "ROUND TWO";
                break;
            case 3:
                killCountText.text = "ROUND THREE";
                break;
            case 4:
                killCountText.text = "ROUND FOUR";
                break;
            case 5:
                killCountText.text = "ROUND FIVE";
                break;
            case 6:
                killCountText.text = "ROUND SIX";
                break;
            case 7:
                killCountText.text = "ROUND SEVEN";
                break;
            case 8:
                killCountText.text = "ROUND EIGHT";
                break;
            case 9:
                killCountText.text = "ROUND NINE";
                break;
            case 10:
                killCountText.text = "ROUND TEN";
                break;
        }
    }
}
