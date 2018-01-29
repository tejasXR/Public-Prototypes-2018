using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public Player playerController;
    private Renderer rend;

    public float alphaHealth = 0; //current alpha representing health
    public float alphaHealthMax; //the maximum alpha value representing a full loss of health
    public float alphaHit = 0; // the alpha addition needed for when the player gets hit

    public GameObject hitbodyProjection; //How the hologram will show

	// Use this for initialization
	void Start () {
        playerController = GameObject.Find("PlayerController").GetComponent<Player>();
        rend = hitbodyProjection.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

        //var alphaPercent = 1 - (playerController.playerHealth / playerController.playerHealthMax);

        //alphaHealth = Mathf.Lerp(alphaHealth, alphaPercent, Time.unscaledDeltaTime) * alphaHealthMax + alphaHit;
        alphaHealth = Mathf.Lerp(alphaHealth, 0, Time.unscaledDeltaTime) * alphaHealthMax + alphaHit;
        alphaHit = Mathf.Lerp(alphaHit, 0, Time.unscaledDeltaTime * 3f);


        rend.material.SetFloat("_Alpha", alphaHealth);

        if (alphaHit <= 0)
        {
            alphaHit = 0;
        }


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            playerController.playerHealth -= 1;
            alphaHit = .1f;
            Destroy(other.gameObject);
        }
    }
}
