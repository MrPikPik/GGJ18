using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class WorldRollOffEditor : MonoBehaviour {
    [Range(-0.01f, 0.01f)]
    public float XRollOff = 0.0f;

    [Range(-0.01f, 0.01f)]
    public float YRollOff = 0.0f;


	void Update () {
        Shader.SetGlobalFloat("curv", XRollOff);
        Shader.SetGlobalFloat("curv2", YRollOff);
    }
}