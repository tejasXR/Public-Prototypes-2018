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
    public TextMeshPro titleText;

    public GameObject wallUIWhole;

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
    }

    // Use this for initialization
    void Start () {

        

        transform.position = UIStartPosition.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

    private void OnEnable()
    {
        transform.position = UIStartPosition.position;
        timerObj.transform.position = timerStartTransform.position;

    }

    // Update is called once per frame
    void Update () {

       
        if (gameManager.roundActive || gameManager.redemptionActive)
        {
            if (isRoundUI)
            {
                alpha = Mathf.SmoothStep(alpha, 1, Time.deltaTime * 2.5f);
            }

            if (isRedemptionUI)
            {
                alpha = Mathf.SmoothStep(alpha, (gameManager.redemptionMeter / gameManager.redemptionMeterMax) - .1f, Time.deltaTime * 5f);
            }

            transform.position = Vector3.Lerp(transform.position, UITargetPosition.position, Time.deltaTime * moveSpeed);

            bufferTime -= Time.deltaTime;
            if (bufferTime <= 0)
            {
                timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerTargetTransform.position, Time.deltaTime);
                bufferTime = 0;
            }
        }
        else //automatically fade out if the player is not in a round
        {
            if (isRoundUI)
            {
                timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerStartTransform.position, Time.deltaTime * 1.5f);
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

        titleText.alpha = alpha;

    }
}
