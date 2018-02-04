using UnityEngine;

public class PlayerController : MonoBehaviour {
    public enum LookDirection {
        NorthEast,
        SouthEast,
        SouthWest,
        NorthWest
    }

    public Animator anim;
    public float baseSpeed = 5.0f;
    public float spriteMaxAngle = 35.0f;

    bool forward = true;
    bool right = true;
    private LookDirection lookDirection;
    public GameObject playerVisuals;

    float deadzone = 0.1f;
    Transform player;

    void Start() {
        this.player = gameObject.transform;
    }

    void Update () {
        Vector3 movementDelta = Vector3.zero;
        if(Mathf.Abs(Input.GetAxis("MoveY")) > deadzone) {
            movementDelta += (player.right * -Input.GetAxis("MoveY"));
        }
        if(Mathf.Abs(Input.GetAxis("MoveX")) > deadzone) {
            movementDelta += (player.forward * -Input.GetAxis("MoveX"));
        }
        player.Translate(movementDelta.normalized * Time.deltaTime * baseSpeed);

        UpdateLookDirection();

        switch(lookDirection) {
            case LookDirection.NorthEast:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = true;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, Input.GetAxis("MoveY") * spriteMaxAngle + 90.0f, 0.0f));
                break;
            case LookDirection.NorthWest:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = false;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, -Input.GetAxis("MoveY") * spriteMaxAngle + 90.0f, 0.0f));
                break;
            case LookDirection.SouthEast:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = true;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, Input.GetAxis("MoveY") * spriteMaxAngle + 90.0f, 0.0f));
                break;
            case LookDirection.SouthWest:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = false;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, -Input.GetAxis("MoveY") * spriteMaxAngle + 90.0f, 0.0f));
                break;
        }

        Vector2 v = new Vector2(Input.GetAxis("MoveX"), Input.GetAxis("MoveY"));
        anim.SetFloat("WalkSpeed", v.magnitude);
    }

    void UpdateLookDirection() {
        if(Input.GetAxis("MoveY") > deadzone) {
            forward = false;
        } else if(Input.GetAxis("MoveY") < -deadzone) {
            forward = true;
        }
        if(Input.GetAxis("MoveX") > deadzone) {
            right = false;
        } else if(Input.GetAxis("MoveX") < -deadzone) {
            right = true;
        }

        if(forward && right) {
            lookDirection = LookDirection.NorthWest;
        } else if(forward && !right) {
            lookDirection = LookDirection.NorthEast;
        } else if(!forward && right) {
            lookDirection = LookDirection.SouthWest;
        } else if(!forward && !right) {
            lookDirection = LookDirection.SouthEast;
        }
    }
}