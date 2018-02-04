using UnityEngine;

[ExecuteInEditMode]
public class WorldRollOffEditor : MonoBehaviour {
    [Range(-10.0f, 10.0f)]
    public float XRollOff = 0.0f;

    [Range(-10.0f, 10.0f)]
    public float YRollOff = 0.0f;
    [Range(1.0f, 10.0f)]
    public float XScale = 1.0f;

    [Range(1.0f, 10.0f)]
    public float YScale = 1.0f;


    void Update () {
        Shader.SetGlobalFloat("curv", XRollOff / 1000);
        Shader.SetGlobalFloat("curv2", YRollOff / 1000);
        Shader.SetGlobalFloat("sclX", XScale);
        Shader.SetGlobalFloat("sclY", YScale);
    }
}