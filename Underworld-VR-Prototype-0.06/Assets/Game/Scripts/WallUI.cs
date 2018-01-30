using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallUI : MonoBehaviour {

    public Transform UITargetPosition;
    public Transform timerTargetTransform;
    public GameObject timerObj;
    public TextMeshPro roundTitle;

    public float moveSpeed;
    public float alpha;
    public float bufferTime;

	// Use this for initialization
	void Start () {
        alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        alpha = Mathf.SmoothStep(alpha, 1, Time.deltaTime * 2.5f);
        roundTitle.alpha = alpha;

        transform.position = Vector3.Lerp(transform.position, UITargetPosition.position, Time.deltaTime * moveSpeed);

        bufferTime -= Time.deltaTime;
        if (bufferTime <= 0)
        {
            timerObj.transform.position = Vector3.Lerp(timerObj.transform.position, timerTargetTransform.position, Time.deltaTime);
            bufferTime = 0;
        }
		
	}
}
