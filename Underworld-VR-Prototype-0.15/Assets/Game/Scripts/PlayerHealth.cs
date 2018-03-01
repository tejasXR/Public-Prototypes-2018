using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerHealth : MonoBehaviour {



    private Player playerController;
    private GameManager gameManager;

    private Renderer rendHitbody;
    //private Renderer rendPlatform;

    public float alphaHealth = 0; //current alpha representing health
    public float alphaHealthMax; //the maximum alpha value representing a full loss of health
    public float alphaHit = 0; // the alpha addition needed for when the player gets hit

    public float healthSmooth;

    public GameObject hitbodyProjection; //How the hologram will show

    //public GameObject[] platformTriangles; // The platform triangle the player is on
    //public GameObject[] belowTriangle1; // The first triangle under player
    //public GameObject[] belowTriangle2;
    //public GameObject[] belowTriangle3;
    //public GameObject[] belowTriangle4;

    public Color[] triangleBlueColors;
    public Color[] triangleRedColors;

    public Color[] triangleCurrentColors;

    public Light platformLight;

    public Material[] belowPlatformNeonMaterial;
    public Material platformTriangleMaterial;
    public Material exteriorPlatformMaterial;


    public AudioSource playerHitSound;
    public AudioSource healthLowSound;

    //private float chromaticAberration;

    // Use this for initialization
    void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        rendHitbody = hitbodyProjection.GetComponent<Renderer>();
        //rendPlatform = platformTriangle.GetComponent<Renderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }
	
	// Update is called once per frame
	void Update () {
        //var chromaticAberration = postProfile.chromaticAberration.settings;
        //chromaticAberration.intensity = Mathf.Lerp(postProfile.chromaticAberration.settings.intensity, 0, Time.deltaTime);
        //postProfile.chromaticAberration.settings = chromaticAberration;

        



        healthSmooth = Mathf.Lerp(healthSmooth, playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier), Time.deltaTime * 3f);

        //var alphaPercent = 1 - (playerController.playerHealth / playerController.playerHealthMax);

        //alphaHealth = Mathf.Lerp(alphaHealth, alphaPercent, Time.unscaledDeltaTime) * alphaHealthMax + alphaHit;
        alphaHealth = Mathf.Lerp(alphaHealth, 0, Time.unscaledDeltaTime) * alphaHealthMax + alphaHit;
        alphaHit = Mathf.Lerp(alphaHit, 0, Time.unscaledDeltaTime * 3f);

        if ((playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier)) < .5f)
        {
            healthLowSound.volume = (1 - (playerController.playerHealth / (playerController.playerHealthMax * playerController.playerHealthMaxMultiplier))) * .5f;
        }


        rendHitbody.material.SetFloat("_Alpha", alphaHealth);

        if (alphaHit <= 0)
        {
            alphaHit = 0;
        }

        //////////////// Watch out for performance issues //////////////////////////


        for (int i = 0; i < 5; i++)
        {
            triangleCurrentColors[i] = Color.Lerp(triangleRedColors[i], triangleBlueColors[i], healthSmooth - .15f);
            //triangleCurrentColors[i] = Color.Lerp(triangleBlueColors[i], triangleRedColors[i], healthPercent);
        }

        /*for (int i = 0; i < platformTriangl.Length; i++)
        {
            platformTriangles[i].GetComponent<Renderer>().material
        }*/

        //rendPlatform.material.SetColor("_MKGlowColor", triangleCurrentColors[4]);
        //rendPlatform.material.SetColor("_MKGlowTexColor", triangleCurrentColors[0]);
        platformLight.color = triangleCurrentColors[0];

        // Here we are directly accessing the materials of the triangle objects, and changing them permanently at runtime
        for (int i = 0; i < 4; i++)
        {
            belowPlatformNeonMaterial[i].SetColor("_MKGlowColor", triangleCurrentColors[i]);
        }

        if (gameManager.inRound)
        {
            platformTriangleMaterial.SetColor("_MKGlowColor", Color.Lerp(platformTriangleMaterial.GetColor("_MKGlowColor"), triangleCurrentColors[0], Time.deltaTime));
            exteriorPlatformMaterial.SetColor("_MKGlowColor", Color.Lerp(platformTriangleMaterial.GetColor("_MKGlowColor"), triangleCurrentColors[0], Time.deltaTime));
        }


        /* ///Here we are changing the material properties of all the triangle parts individually at runtime
        for (int i = 1; i < 4; i++)
        {
            //belowPlatformNeon = GetComponent<Renderer>().materials;
            for (int j = 0; j < 3; j++)
            {
                belowTriangle1[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
                //belowTriangle1[j].GetComponent<Renderer>().material.SetColor("_MKGlowTexColor", triangleCurrentColors[i]);

                belowTriangle2[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
                //belowTriangle2[j].GetComponent<Renderer>().material.SetColor("_MKGlowTexColor", triangleCurrentColors[i]);

                belowTriangle3[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
                //belowTriangle3[j].GetComponent<Renderer>().material.SetColor("_MKGlowTexColor", triangleCurrentColors[i]);

                belowTriangle4[j].GetComponent<Renderer>().material.SetColor("_MKGlowColor", triangleCurrentColors[i]);
            }
            
        }*/
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            playerHitSound.Play();

            //var chromaticAberration = postProfile.chromaticAberration.settings;
            //chromaticAberration.intensity = 1;
            //postProfile.chromaticAberration.settings = chromaticAberration;

            if (gameManager.roundActive)
            {
                
                playerController.playerHealth -= other.gameObject.GetComponent<EnemyBullet>().damage;
                alphaHit = .1f;
                Destroy(other.gameObject);
            }

            /*if (gameManager.redemptionActive)
            {
                playerController.playerRedemptionHealth -= ;
                alphaHit = .1f;
                Destroy(other.gameObject);
            }*/
        }

        if (other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<EnemyMovement>().isBomberDrone)
        {
            if (gameManager.roundActive)
            {
                playerController.playerHealth -= 5;
                alphaHit = .1f;
                //Destroy(other.gameObject);
            }
        }
        {

        }
    }
}
