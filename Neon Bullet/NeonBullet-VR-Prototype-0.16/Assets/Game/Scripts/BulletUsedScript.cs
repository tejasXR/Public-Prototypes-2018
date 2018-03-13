using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletUsedScript : MonoBehaviour {

    private Rigidbody rb;
    public TextMeshPro[] texts;
    public float xForceMin;
    public float xForceMax;

    public float yForceMin;
    public float yForceMax;

    public float zForceMin;
    public float zForceMax;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        rb.velocity = new Vector3(Random.Range(xForceMin, xForceMax), Random.Range(yForceMin, yForceMax), Random.Range(zForceMin, zForceMax));

        
         /*   for (int i = 0; i < texts.Length; i++)
            {
                if (texts[i].alpha >= 0)
                {
                    texts[i].alpha -= Time.deltaTime / 2f;
                    if (texts[i].alpha <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                }
            }*/
               


              
	}
	
	// Update is called once per frame
	void Update () {

    if (texts.Length > 0)
    {
        foreach (TextMeshPro text in texts)
        {
            text.alpha -= Time.deltaTime;
            if (text.alpha <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
}
