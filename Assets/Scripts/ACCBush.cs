using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACCBush : MonoBehaviour {
    public float growthTime = 15.0f;
    private float growthTimer = 0.0f;
    private bool inInteractionRange = false;
    public Transform fruitSpot;
    public Material[] leaveMaterials;
    public MeshRenderer[] leaveMeshes;
    public Material[] fruitMaterials;
    public MeshRenderer fruitSprite;

    void Start() {
        growthTimer = 0.0f;

        foreach(MeshRenderer m in leaveMeshes) {
            m.material = leaveMaterials[Random.Range(0, leaveMaterials.Length - 1)];
        }
        fruitSprite.material = fruitMaterials[Random.Range(0, fruitMaterials.Length - 1)];
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
        //Grow fruit
        if(growthTimer < growthTime) {
            growthTimer += Time.deltaTime;
            fruitSpot.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
        }

        //Interaction
        if(Input.GetButtonDown("Interact") && growthTimer >= growthTime && inInteractionRange) {
            Harvest();
        }
    }

    void Harvest() {
        GameManager.PlayPickupSound();
        fruitSpot.transform.localScale = Vector3.zero;
        GameManager.AwardResource(GameManager.ResourceType.FruitBlue, 2);
        growthTimer = 0.0f;   
    }
}