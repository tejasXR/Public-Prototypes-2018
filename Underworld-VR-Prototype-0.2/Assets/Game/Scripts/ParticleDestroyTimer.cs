using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyTimer : MonoBehaviour {

    public float destroyInSeconds;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyInSeconds);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
