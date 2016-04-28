using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController))]
public class RelativeMovement : MonoBehaviour {

    [SerializeField] private Transform target;
    private CharacterController _charController;

    public float rotSpeed = 15.0f;
    public float movSpeed = 6.0f;

    void Start () {
        _charController = GetComponent<CharacterController> ();
    }

    void Update() {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis ("Horizontal");
        float vertInput = Input.GetAxis ("Vertical");
        if (horInput != 0 || vertInput != 0) {
            movement.x = horInput * movSpeed;
            movement.z = vertInput * movSpeed;
            movement = Vector3.ClampMagnitude (movement, movSpeed);
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
            movement = target.TransformDirection (movement);
            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation (movement);
            transform.rotation = Quaternion.Lerp (transform.rotation, direction, rotSpeed * Time.deltaTime);
        }
        movement *= Time.deltaTime;
        _charController.Move (movement);
    }

}
