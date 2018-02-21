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

    private Renderer rend;

    public Color32 healthColorOriginal;
    public Color32 gainHealthColor;
    public Color32 loseHealthColor;

    public Color32 healthIconColorOriginal;

    public Image healthIcon;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Use this for initialization
    void Start () {

        

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

                rend.material.SetFloat("_Alpha", 1);
                rend.material.SetColor("_MainColor", gainHealthColor);

                if (hasHealthIcon)
                {
                    healthIcon.material.SetColor("_Color", gainHealthColor);
                }
            }



        } /*else
        {
            //healthSmoothPercent -= Time.unscaledDeltaTime * 3f;


            //if (healthSmoothPercent < (playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier)))
            {
                rend.material.SetFloat("_Alpha", 1);
                rend.material.SetColor("_MainColor", loseHealthColor);

                if (hasHealthIcon)
                {
                    healthIcon.material.SetColor("_Color", loseHealthColor);
                }
            }
        }*/


        rend.material.SetFloat("_Alpha", Mathf.Lerp(rend.material.GetFloat("_Alpha"), 1.1f - healthSmoothPercent, Time.deltaTime * 2f));
        rend.material.SetColor("_MainColor", Color.Lerp(rend.material.GetColor("_MainColor"), healthColorOriginal, Time.deltaTime * 2f));
        healthIcon.material.SetColor("_Color", Color.Lerp(healthIcon.material.GetColor("_Color"), healthIconColorOriginal, Time.deltaTime * 2f));

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
