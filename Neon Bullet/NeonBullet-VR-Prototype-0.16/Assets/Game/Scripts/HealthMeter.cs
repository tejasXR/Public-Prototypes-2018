using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool adjust;

    public bool hasHealthIcon;
    public bool hasHealthIconOutline;

    public bool healthLowFlash;
    public float healthFlashDuration;
    public float healthFlashDurationTimer;

    private Renderer rend;

    public Color32 healthColorOriginal;
    public Color32 gainHealthColor;
    public Color32 loseHealthColor;

    public Color32 healthIconColorOriginal;

    public Image healthIcon;

    public float textSolidTimer;
    public float textSolidTimerDuration;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {


        healthFlashDurationTimer = healthFlashDuration;
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
        healthSmoothPercent = 0;

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

        healthSmoothPercent = Mathf.Lerp(healthSmoothPercent, playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier), Time.deltaTime * 4f);

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

        if (adjust)
        {

            //if (healthSmoothPercent <= (playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier)))
            {
                //healthSmoothPercent += Time.unscaledDeltaTime * 3f;

                //rend.material.SetFloat("_Alpha", 1);

                if (healthSmoothPercent <= (playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier)))
                {
                    rend.material.SetColor("_Color", gainHealthColor);

                    if (hasHealthIcon)
                    {
                        healthIcon.material.SetColor("_Color", gainHealthColor);
                    }
                } else
                {
                    rend.material.SetColor("_Color", loseHealthColor);

                    if (hasHealthIcon)
                    {
                        healthIcon.material.SetColor("_Color", loseHealthColor);
                    }
                }
            }



        }/* else
        {
            //healthSmoothPercent -= Time.unscaledDeltaTime * 3f;


            if (healthSmoothPercent > (playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier)))
            {
                //rend.material.SetFloat("_Alpha", 1);
                
            }
        }*/

        if ((playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier) < .25f))
        {
            //healthLowFlash = true;
            if (!healthLowFlash)
            {
                rend.material.SetColor("_Color", loseHealthColor);

                if (hasHealthIcon)
                {
                    healthIcon.material.SetColor("_Color", loseHealthColor);
                }

                healthLowFlash = true;
            }
            
            healthFlashDurationTimer -= Time.deltaTime;
            if (healthFlashDurationTimer <= 0)
            {
                healthLowFlash = false;
                healthFlashDurationTimer = healthFlashDuration;
            }
        } else
        {
            healthLowFlash = false;
        }




        //rend.material.SetFloat("_Alpha", Mathf.Lerp(rend.material.GetFloat("_Alpha"), 1.1f - healthSmoothPercent, Time.deltaTime));
        rend.material.SetColor("_Color", Color.Lerp(rend.material.GetColor("_Color"), healthColorOriginal, Time.deltaTime));
        healthIcon.material.SetColor("_Color", Color.Lerp(healthIcon.material.GetColor("_Color"), healthIconColorOriginal, Time.deltaTime));

        //transform.localScale = new Vector3(scaleCurrent, transform.localScale.y, transform.localScale.z);
        //transform.localPosition = new Vector3(meterXCurrent, transform.localPosition.y, transform.localPosition.z);

        if (Mathf.Abs(healthSmoothPercent - (playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier))) > .01f)
        {
            adjust = true;
        }
        else
        {
            adjust = false;
        }

    }
}
