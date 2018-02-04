using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTree : MonoBehaviour {
    public Material[] leaveMaterials;
    public MeshRenderer[] leaveMeshes;

    void Start() {
        foreach(MeshRenderer m in leaveMeshes) {
            m.material = leaveMaterials[Random.Range(0, leaveMaterials.Length - 1)];
        }
    }
}