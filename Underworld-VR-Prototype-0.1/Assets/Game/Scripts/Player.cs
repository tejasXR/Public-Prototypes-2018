using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float playerHealth;
    public TextMesh healthText;
    //public string health;

	// Use this for initialization
	void Start () {
        healthText.text = "" + playerHealth.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		if (healthText.text != playerHealth.ToString())
        {
            healthText.text = "" + playerHealth.ToString();
        }
	}
}
