using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletTextCounter : MonoBehaviour {

    private Player playerController;
    public float bulletSmoothCount;
    private TextMeshPro bulletTextCounter;
    public float countSpeed = 85f;
    private bool adjust;

    public bool formatXX;
    public bool formatXXX;
    public bool hasBulletIcon;
    public bool hasBulletIconOutline;
    public bool changeTextColor;

    public Color32 textColorOriginal;
    public Color32 textColorGainBullet;
    public Color32 textColorLoseBullet;

    public Color32 bulletIconOriginal;
    public Color32 bulletIconOutlineOriginal;

    public Image bulletIcon;
    public Image bulletIconOutline;

    public float textSolidTimer;
    public float textSolidTimerDuration;

    //public bool formatXXX;

    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        bulletTextCounter = GetComponent<TextMeshPro>();
        //textColorOriginal = bulletTextCounter.color;

    }

    private void OnEnable()
    {
        bulletSmoothCount = 0;
    }

    // Update is called once per frame
    void Update () {

        if (adjust)
        {
            if (bulletSmoothCount <= playerController.playerBullets)
            {
                bulletSmoothCount += Time.unscaledDeltaTime * countSpeed;

                if (changeTextColor)
                {
                    bulletTextCounter.color = textColorGainBullet;

                    if (hasBulletIcon)
                    {
                        //bulletIcon.GetComponent<Renderer>().material.SetColor("_Color", textColorGainBullet);
                        bulletIcon.material.SetColor("_Color", textColorGainBullet);


                        if (hasBulletIconOutline)
                        {
                            bulletIconOutline.material.SetColor("_Color", textColorGainBullet);
                        }
                    }
                }

                

            }
            else
            {
                bulletSmoothCount -= Time.unscaledDeltaTime * countSpeed;

                if (changeTextColor)
                {
                    bulletTextCounter.color = textColorLoseBullet;

                    if (hasBulletIcon)
                    {
                        //bulletIcon.GetComponent<Renderer>().material.SetColor("_Color", textColorLoseBullet);
                        bulletIcon.material.SetColor("_Color", textColorLoseBullet);


                        if (hasBulletIconOutline)
                        {
                            bulletIconOutline.material.SetColor("_Color", textColorLoseBullet);
                        }
                    }
                }

                

            }
        }

        if (Mathf.Abs(bulletSmoothCount - playerController.playerBullets) > .5f)
        {
            adjust = true;
            textSolidTimerDuration = textSolidTimer;
        } else
        {
            adjust = false;
        }

        //bulletTextCounter.color = Color.Lerp(bulletTextCounter.color, textColorOriginal, Time.deltaTime * 2f);
        //bulletIcon.material.SetColor("_Color", Color.Lerp(bulletIcon.material.GetColor("_Color"), bulletIconOriginal, Time.deltaTime));
        textSolidTimerDuration -= Time.deltaTime;
        if (textSolidTimerDuration <= 0)
        {
            if (changeTextColor)
            {
                bulletTextCounter.alpha = Mathf.Lerp(bulletTextCounter.alpha, 0, Time.deltaTime * 5f);

                if (hasBulletIcon)
                {
                    bulletIcon.material.SetColor("_Color", Color.Lerp(bulletIcon.material.GetColor("_Color"), bulletIconOriginal, Time.deltaTime * 5f));
                }

            }

           

        }
        //bulletIcon.GetComponent<Renderer>().material.SetColor("_Color", Color.Lerp(bulletIcon.GetComponent<Renderer>().material.GetColor("_Color"), bulletIconOriginal, Time.deltaTime * 3f));
        

        //bulletIconOutline.material.SetColor("_Color", Color.Lerp(bulletIconOutline.material.GetColor("_Color"), bulletIconOutlineOriginal, Time.deltaTime * 3f));

        //bulletSmoothCount = Mathf.RoundToInt(Mathf.Lerp(bulletSmoothCount, playerController.playerBullets, Time.deltaTime * countSpeed));

        if (formatXX)
        {
            bulletTextCounter.text = "" + Mathf.RoundToInt(bulletSmoothCount).ToString("00");
        } else if (formatXXX)
        {
            bulletTextCounter.text = "" + Mathf.RoundToInt(bulletSmoothCount).ToString("000");
        } else
        {
            bulletTextCounter.text = "" + Mathf.RoundToInt(bulletSmoothCount).ToString("0000");
        }
    }
}
