using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFPC : MonoBehaviour
{
    [SerializeField] private float _speed = 7.5f;
    [SerializeField] private float _jumpSpeed = 8.0f;
    [SerializeField] private float _gravity = 20.0f;
    [SerializeField] private Transform _playerCameraParent;
    [SerializeField] private float _lookSpeed = 2.0f;
    [SerializeField] private float _lookXLimit = 60.0f;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private Vector2 _rotation = Vector2.zero;

    private bool _inputActive = true;

    private AttentionSource _attentionSource;
    [SerializeField] private SFXPlayer _sfxPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _attentionSource = gameObject.GetComponent<AttentionSource>();
        _characterController = GetComponent<CharacterController>();
        _rotation.y = transform.eulerAngles.y;

        //Lock cursor
        EnableInput();

        GameManager.Instance.OnGameEnd += DisableInput;
        GameManager.Instance.OnGamePause += DisableInput;
        GameManager.Instance.OnGameResume += EnableInput;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_inputActive) return;

        if (_characterController.isGrounded)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            Vector2 curSpeed = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * _speed;
            _moveDirection = (forward * curSpeed.x) + (right * curSpeed.y);

            if (Input.GetButton("Jump"))
            {
                _moveDirection.y = _jumpSpeed;
                _attentionSource.TryActivateSource();
                _sfxPlayer.PlayDogBarkSound();
            }
        }

        _moveDirection.y -= _gravity * Time.deltaTime;

        _characterController.Move(_moveDirection * Time.deltaTime);

        // Player and Camera rotation

        _rotation.y += Input.GetAxis("Mouse X") * _lookSpeed;
        _rotation.x += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotation.x = Mathf.Clamp(_rotation.x, -_lookXLimit, _lookXLimit);
        _playerCameraParent.localRotation = Quaternion.Euler(_rotation.x, 0, 0);
        transform.eulerAngles = new Vector2(0, _rotation.y);
    }

    private void EnableInput()
    {
        _inputActive = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void DisableInput()
    {
        _inputActive = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
