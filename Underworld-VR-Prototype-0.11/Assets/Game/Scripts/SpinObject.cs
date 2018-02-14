using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour {

    public bool spinAlongX;
    public bool spinAlongY;
    public bool spinAlongZ;

    public float spinSpeed;

    public bool useRigidbody;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!useRigidbody)
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

    private void FixedUpdate()
    {
        if (useRigidbody)
        {

            if (spinAlongX)
            {
                Quaternion rot = Quaternion.Euler(1 * Time.deltaTime * spinSpeed, 0, 0);
                rb.MoveRotation(rb.rotation * rot);
            }
            else if (spinAlongY)
            {
                Quaternion rot = Quaternion.Euler(0, 1 * Time.deltaTime * spinSpeed, 0);
                rb.MoveRotation(rb.rotation * rot);

            }
            else if (spinAlongZ)
            {
                Quaternion rot = Quaternion.Euler(0, 0, 1 * Time.deltaTime * spinSpeed);
                rb.MoveRotation(rb.rotation * rot);

            }


        }
    }
}
