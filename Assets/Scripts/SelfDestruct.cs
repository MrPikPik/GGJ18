using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    public float time = 10.0f;
    private float timer = 0.0f;
	void Update () {
        timer += Time.deltaTime;
        if(timer >= time) {
            Destroy(gameObject);
        }
	}
}