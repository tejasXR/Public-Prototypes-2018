using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumScale : MonoBehaviour {

    public float scaleCurrent;
    public float scaleOriginal;


	// Use this for initialization
	void Start () {
        scaleOriginal = transform.localScale.y;
        scaleCurrent = .6f;
	}
	
	// Update is called once per frame
	void Update () {

        scaleCurrent = Mathf.Lerp(scaleCurrent, scaleOriginal, Time.deltaTime);
        transform.localScale = new Vector3(transform.localScale.x, scaleCurrent, transform.localScale.z);

        if ((scaleCurrent / scaleOriginal) > .98f)
        {
            Destroy(this);
        }
		
	}
}
