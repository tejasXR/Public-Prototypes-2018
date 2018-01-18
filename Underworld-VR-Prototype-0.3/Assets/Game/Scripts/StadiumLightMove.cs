using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumLightMove : MonoBehaviour {

    public float xAngleMin;
    public float xAngleMax;

    public float yAngleMin;
    public float yAngleMax;

    public float zAngleMin;
    public float zAngleMax;

    public float moveFrequency;
    private float moveFrequencyTimer;

    public float moveSpeed;

    private Quaternion rotation;

    // Use this for initialization
    void Start () {
        moveFrequencyTimer = moveFrequency;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, rotation, Time.deltaTime * moveSpeed);


        moveFrequencyTimer -= Time.deltaTime;
        if (moveFrequencyTimer <= 0)
        {
            RandomAngle();
            moveFrequencyTimer = moveFrequency;
        }

	}

    void RandomAngle()
    {
        var x = Random.Range(xAngleMin, xAngleMax);
        var y = Random.Range(yAngleMin, yAngleMax);
        var z = Random.Range(zAngleMin, zAngleMax);

        rotation = Quaternion.Euler(new Vector3(x, y, z));
    }

}
