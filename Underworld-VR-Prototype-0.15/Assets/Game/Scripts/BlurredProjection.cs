using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurredProjection : MonoBehaviour {

    public GameObject player; //player Eye object

	// Use this for initialization
	void Start () {
		
	}

    private void OnEnable()
    {
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
