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

    void FixedUpdate () {
        Vector3 movementDelta = Vector3.zero;
        if(Mathf.Abs(Input.GetAxis("MoveY")) > deadzone) {
            movementDelta += player.right * -Input.GetAxis("MoveY");
        } else if(Input.GetButton("Forward")) {
            movementDelta += player.right * Input.GetAxis("Forward");
        }
        if(Mathf.Abs(Input.GetAxis("MoveX")) > deadzone) {
            movementDelta += player.forward * -Input.GetAxis("MoveX");
        } else if(Input.GetButton("Right")) {
            movementDelta += player.forward * -Input.GetAxis("Right");
        }

        player.Translate(movementDelta.normalized * Time.deltaTime * baseSpeed);

        UpdateLookDirection();

        switch(lookDirection) {
            case LookDirection.NorthEast:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = true;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (Input.GetAxis("MoveY") + -Input.GetAxis("Forward")) * spriteMaxAngle + 90.0f, 0.0f));
                break;
            case LookDirection.NorthWest:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = false;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (-Input.GetAxis("MoveY") + Input.GetAxis("Forward")) * spriteMaxAngle + 90.0f, 0.0f));
                break;
            case LookDirection.SouthEast:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = true;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (Input.GetAxis("MoveY") + -Input.GetAxis("Forward")) * spriteMaxAngle + 90.0f, 0.0f));
                break;
            case LookDirection.SouthWest:
                playerVisuals.GetComponent<SpriteRenderer>().flipX = false;
                playerVisuals.transform.rotation = Quaternion.Euler(new Vector3(0.0f, (-Input.GetAxis("MoveY") + Input.GetAxis("Forward")) * spriteMaxAngle + 90.0f, 0.0f));
                break;
        }

        Vector2 v = new Vector2(Input.GetAxis("MoveX") + Input.GetAxis("Forward"), Input.GetAxis("MoveY") + Input.GetAxis("Right"));
        anim.SetFloat("WalkSpeed", v.magnitude);
    }

    void UpdateLookDirection() {
        if(Input.GetAxis("MoveY") > deadzone || Input.GetAxis("Forward") > 0.0f) {
            forward = false;
        } else if(Input.GetAxis("MoveY") < -deadzone || Input.GetAxis("Forward") < 0.0f) {
            forward = true;
        }
        if(Input.GetAxis("MoveX") > deadzone || Input.GetAxis("Right") > 0.0f) {
            right = false;
        } else if(Input.GetAxis("MoveX") < -deadzone || Input.GetAxis("Right") < 0.0f) {
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