using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPC : MonoBehaviour
{
    public float speed = 7.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Transform playerCameraParent;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 60.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    Vector2 rotation = Vector2.zero;

    private AttentionSource _attentionSource;

    // Start is called before the first frame update
    void Start()
    {
        _attentionSource = gameObject.GetComponent<AttentionSource>();
        characterController = GetComponent<CharacterController>();
        rotation.y = transform.eulerAngles.y;

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction based on axes
            // Vector3 forward = transform.TransformDirection(Vector3.forward);
            // Vector3 right = transform.TransformDirection(Vector3.right);

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            float curSpeedX = speed * Input.GetAxis("Vertical");
            float curSpeedY = speed * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
                _attentionSource.ActivateSource();
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation

        rotation.y += Input.GetAxis("Mouse X") * lookSpeed;
        rotation.x += -Input.GetAxis("Mouse Y") * lookSpeed;
        rotation.x = Mathf.Clamp(rotation.x, -lookXLimit, lookXLimit);
        playerCameraParent.localRotation = Quaternion.Euler(rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, rotation.y);

    }
}
