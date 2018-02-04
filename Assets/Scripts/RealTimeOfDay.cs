using UnityEngine;

public class RealTimeOfDay : MonoBehaviour {
    public float rotationSpeed = 5.0f;
	void Update () {
        gameObject.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}