using UnityEngine;

public class ACCFruit : MonoBehaviour {
    private bool inInteractionRange = false;
    public int score = 50;
    public Material[] materials;
    public MeshRenderer imagePlane;
    public GameManager.ResourceType type;

    void Start() {
        imagePlane.material = materials[Random.Range(0, materials.Length - 1)];
    }

    void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            inInteractionRange = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.tag == "Player") {
            inInteractionRange = false;
        }
    }

    void Update() {
        //Interaction
        if(Input.GetButtonDown("Interact") && inInteractionRange) {
            GameManager.AwardScore(score);
            GameManager.AwardResource(type, 1);
            GameManager.PlayPickupSound();
            Destroy(gameObject);
        }
    }
}