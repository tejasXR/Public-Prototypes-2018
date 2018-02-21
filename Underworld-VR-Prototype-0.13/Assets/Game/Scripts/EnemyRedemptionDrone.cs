using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRedemptionDrone : MonoBehaviour {

    public EnemyParent enemyParent;


    // Use this for initialization
    void Start () {
        enemyParent = GetComponent<EnemyParent>();

    }

    // Update is called once per frame
    void Update () {

        transform.LookAt(enemyParent.player.transform.position);

        if (!enemyParent.gameManager.redemptionActive)
        {
            Destroy(this.gameObject);
        }
	}

    
}
