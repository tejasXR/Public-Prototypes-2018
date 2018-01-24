using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour {

    public bool spinAlongX;
    public bool spinAlongY;
    public bool spinAlongZ;

    public float spinSpeed;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spinAlongY)
        {
            transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
        }
        else if (spinAlongX)
        {
            transform.Rotate(Vector3.right, spinSpeed * Time.deltaTime);
        }
        else if (spinAlongZ)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
    }
}
