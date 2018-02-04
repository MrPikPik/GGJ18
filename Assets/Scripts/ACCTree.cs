using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ACCTree : MonoBehaviour {
    private int noOfFruit;
    public float growthTime = 15.0f;
    private float growthTimer = 0.0f;
    private bool inInteractionRange = false;
    public Transform fruitSpot1;
    public Transform fruitSpot2;
    public Transform fruitSpot3;
    public GameObject FruitPrefab;
    public Material[] leaveMaterials;
    public MeshRenderer[] leaveMeshes;
    public Material[] fruitMaterials;
    public MeshRenderer[] fruitSprites;

    void Start () {
        noOfFruit = Random.Range(1, 4);
        growthTimer = 0.0f;

        foreach(MeshRenderer m in leaveMeshes) {
            m.material = leaveMaterials[Random.Range(0, leaveMaterials.Length - 1)];
        }
        foreach(MeshRenderer m in fruitSprites) {
            m.material = fruitMaterials[Random.Range(0, fruitMaterials.Length - 1)];
        }
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

    void Update () {
        //Grow fruit
        if(growthTimer < growthTime) {
            growthTimer += Time.deltaTime;
            switch(noOfFruit) {
                case 1:
                    fruitSpot1.transform.localScale = Vector3.zero;
                    fruitSpot2.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
                    fruitSpot3.transform.localScale = Vector3.zero;
                    break;
                case 2:
                    fruitSpot1.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
                    fruitSpot2.transform.localScale = Vector3.zero;
                    fruitSpot3.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
                    break;
                case 3:
                    fruitSpot1.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
                    fruitSpot2.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
                    fruitSpot3.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, growthTimer / growthTime);
                    break;
            }       
        }

        //Interaction
        if(Input.GetButtonDown("Interact") && growthTimer >= growthTime && inInteractionRange) {
            Harvest();
        }
    }

    void Harvest() {
        StartCoroutine("TreeJiggle");
        fruitSpot1.transform.localScale = Vector3.zero;
        fruitSpot2.transform.localScale = Vector3.zero;
        fruitSpot3.transform.localScale = Vector3.zero;
        
        switch(noOfFruit){
            case 1:
                Instantiate(FruitPrefab, fruitSpot2.transform.position, fruitSpot2.rotation, null);
                break;
            case 2:
                Instantiate(FruitPrefab, fruitSpot1.transform.position, fruitSpot1.rotation, null);
                Instantiate(FruitPrefab, fruitSpot3.transform.position, fruitSpot3.rotation, null);
                break;
            case 3:
                Instantiate(FruitPrefab, fruitSpot1.transform.position, fruitSpot1.rotation, null);
                Instantiate(FruitPrefab, fruitSpot2.transform.position, fruitSpot2.rotation, null);
                Instantiate(FruitPrefab, fruitSpot3.transform.position, fruitSpot3.rotation, null);
                break;
        }

        growthTimer = 0.0f;
        noOfFruit = Random.Range(1, 4);
    }

    IEnumerator TreeJiggle() {
        bool enabled = true;
        while(enabled) {
            GetComponent<Animator>().enabled = true;
            yield return new WaitForSeconds(0.5f);
            enabled = false;
        }
        GetComponent<Animator>().enabled = false;
    }
}