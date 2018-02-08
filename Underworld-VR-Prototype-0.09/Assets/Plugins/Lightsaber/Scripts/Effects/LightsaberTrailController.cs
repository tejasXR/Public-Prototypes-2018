using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller class which uses the LightsaberTrail class to show a trail when we move the lightsaber
/// </summary>
[RequireComponent(typeof(LightsaberTrail))]
public class LightsaberTrailController : MonoBehaviour {

    LightsaberTrail lightsaberTrail;

    void Start()
    {
        lightsaberTrail = GetComponent<LightsaberTrail>();
    }

    void Update()
    {
        lightsaberTrail.Iterate(Time.unscaledTime);
        lightsaberTrail.UpdateTrail(Time.unscaledTime, 0f);
    }

}
