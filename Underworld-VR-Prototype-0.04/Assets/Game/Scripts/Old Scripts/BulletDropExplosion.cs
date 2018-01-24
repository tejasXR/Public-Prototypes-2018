using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDropExplosion : MonoBehaviour {

    private Rigidbody rb;
    public float explosionPower = 10f;
    float explosioRadius = 5f;

    // Use this for initialization
    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosioRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionPower, explosionPos, explosioRadius);
        }

    }
}
