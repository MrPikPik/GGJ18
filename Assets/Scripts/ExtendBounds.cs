using UnityEngine;

public class ExtendBounds : MonoBehaviour {
	void Start () {
        foreach(Mesh mesh in gameObject.GetComponentsInChildren<Mesh>()){
            Bounds b = mesh.bounds;
            b.extents *= 2.0f;
        }
	}
}