using UnityEngine;

public class ACCFlower : MonoBehaviour {
    public float growTime = 20.0f;
    private float growTimer = 0.0f;
    public Material[] flowerMats;
    public Transform rootPoint;
    private bool planted = false;
    public GameObject plantPlane;
    public GameObject flowerPlane;
    public int score = 100;
    private bool inInteractionRange = false;

    void Update() {
        if(Input.GetButtonDown("Interact") && planted && inInteractionRange && growTimer >= growTime) {
            Harvest();
        } else if(Input.GetButtonDown("Interact") && !planted && inInteractionRange) {
            Plant();
        }

        if(growTimer < growTime && planted) {
            float progress = growTimer / growTime;

            //Fully grow plant over the course of growtime
            plantPlane.transform.position = Vector3.Lerp(rootPoint.position, gameObject.transform.position, progress * 1.33f);

            //If 75% done, start growing flower itself
            if(progress > 0.75f) {
                flowerPlane.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, (progress - 0.75f) * 4.0f);
            }


            growTimer += Time.deltaTime;
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

    void Harvest() {
        planted = false;
        plantPlane.transform.position = rootPoint.position - Vector3.one;
        GameManager.AwardScore(score);
        GameManager.AwardResource(GameManager.ResourceType.Flower, 1);
    }

    void Plant() {
        flowerPlane.GetComponentInChildren<MeshRenderer>().material = flowerMats[Random.Range(0, flowerMats.Length -1)];
        growTimer = 0.0f;
        planted = true;
        plantPlane.transform.position = rootPoint.position;
        flowerPlane.transform.localScale = Vector3.zero;
    }
}