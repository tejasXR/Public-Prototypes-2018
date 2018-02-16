using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyeGlow : MonoBehaviour {

    private Renderer rend;

    public float glowPowerTarget;
    public float textureStrengthTarget;
    public Color colorTarget;

    public float lerpSpeed;

    private float glowPowerOriginal;
    private float textureStrengthOriginal;
    private Color colorOriginal;

    private float glowPowerCurrent;
    private float textureStrengthCurrent;
    private Color colorCurrent;

    public bool beforeAttack;

    

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();

        glowPowerOriginal = rend.material.GetFloat("_MKGlowPower");
        textureStrengthOriginal = rend.material.GetFloat("_MKGlowTexStrength");
        colorOriginal = rend.material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (beforeAttack)
        {
            glowPowerCurrent = Mathf.SmoothStep(glowPowerCurrent, glowPowerTarget, Time.deltaTime * lerpSpeed);
            textureStrengthCurrent = Mathf.SmoothStep(textureStrengthCurrent, textureStrengthTarget, Time.deltaTime * lerpSpeed);
            colorCurrent = Color.Lerp(colorCurrent, colorTarget, Time.deltaTime * lerpSpeed);

        } else
        {
            glowPowerCurrent = Mathf.SmoothStep(glowPowerCurrent, glowPowerOriginal, Time.deltaTime * lerpSpeed);
            textureStrengthCurrent = Mathf.SmoothStep(textureStrengthCurrent, textureStrengthOriginal, Time.deltaTime * lerpSpeed);
            colorCurrent = Color.Lerp(colorCurrent, colorOriginal, Time.deltaTime * lerpSpeed);
        }
        

        rend.material.SetFloat("_MKGlowPower", glowPowerCurrent);
        rend.material.SetFloat("_MKGlowTexStrength", textureStrengthCurrent);
        rend.material.SetColor("_Color", colorCurrent);
	}

    public void Flash()
    {
        glowPowerCurrent = glowPowerTarget;
        textureStrengthCurrent = textureStrengthTarget;
        colorCurrent = colorTarget;
    }
}
