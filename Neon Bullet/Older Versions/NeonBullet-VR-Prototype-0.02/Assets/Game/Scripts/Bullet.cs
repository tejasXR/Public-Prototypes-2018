using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float damage;
    public GameObject bulletHitEffect;

    private void OnDestroy()
    {
        Instantiate(bulletHitEffect, transform.position, transform.rotation);
    }
}
