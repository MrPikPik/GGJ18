using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityAdjust : MonoBehaviour {
	void Update () {
		Physics.gravity = (Vector3.zero - gameObject.transform.position).normalized * 9.81f;
	}
}