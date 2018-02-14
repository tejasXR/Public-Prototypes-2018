using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCounterFollow : MonoBehaviour {

    public GameObject bulletCounterPoint;

    public float moveSpeed;
    public float rotationSpeed;

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, bulletCounterPoint.transform.position, Time.unscaledDeltaTime * moveSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, bulletCounterPoint.transform.rotation, Time.unscaledDeltaTime * rotationSpeed);
    }
}
