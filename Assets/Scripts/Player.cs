using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Links")]
    public Camera Camera => _camera;

    [Header("CORE")]
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Camera _camera;

    [Header("Look")]
    [SerializeField] private float _mouseSensivity = 1f;
    [SerializeField][Range(30, 113)] private float _fieldOfView = 90f;
    [SerializeField] private Vector3 _cameraOffset = new Vector3(0f, 1.2f, 0f);
    private Vector2 _cameraInput;
    private float _cameraVerticalAngle = 0f;

    [Header("Movement")]
    [SerializeField] private float _walkSpeed = 1f;
    [SerializeField] private float _runSpeed = 3f; // ? need or not
    private Vector2 _moveInput;
    // maybe add _moveSpeed that need to be filled for max speed by time

    [Header("Jump")]
    [SerializeField] private float _jumpPower = 3f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _gravityMultiplier = 1f;
    private float _velocity_Y = 0;
    private bool _isJumping;
    public bool IsGrounded => _controller.isGrounded;

    private void Update()
    {
        HandleRotation();
        SetGravity();

        HandleJump();
        HandleMovement();

#if UNITY_EDITOR
        _camera.fieldOfView = _fieldOfView;
        _camera.transform.localPosition = _cameraOffset;
#endif
    }

    #region Input
    public void OnLookInput(InputAction.CallbackContext context)
    {
        _cameraInput = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        _isJumping = context.ReadValueAsButton();
        // maybe invoke action/unitask for double jump etc
    }
    #endregion

    #region Handle
    private void SetGravity()
    {
        if (IsGrounded && !_isJumping)
        {
            _velocity_Y = -1f;
        }
        else
        {
            _velocity_Y += _gravity * _gravityMultiplier * Time.deltaTime; // negative
        }
    }

    private void HandleRotation()
    {
        float mouseX = _cameraInput.x * _mouseSensivity;
        float mouseY = _cameraInput.y * _mouseSensivity;

        _cameraVerticalAngle -= mouseY;
        _cameraVerticalAngle = Mathf.Clamp(_cameraVerticalAngle, -90f, 90f);

        _camera.transform.localRotation = Quaternion.Euler(_cameraVerticalAngle, 0, 0);
        this.transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleJump()
    {
        if (_isJumping && IsGrounded)
        {
            _velocity_Y = _jumpPower;
        }
    }

    private void HandleMovement()
    {
        Vector3 move = (
            this.transform.right * _moveInput.x * _walkSpeed
            + this.transform.up * _velocity_Y
            + this.transform.forward * _moveInput.y * _walkSpeed
            );

        _controller.Move(move * Time.deltaTime);
    }

    #endregion

    InputSystem_Actions _inputs;
    private void Awake()
    {
        _inputs = new();
        InitInputs(this);

        _inputs.Enable();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDestroy()
    {
        _inputs?.Disable();
        _inputs?.Dispose();
        RemoveInputs(this);
    }

    public void InitInputs(Player playerController)
    {
        _inputs.Player.Move.performed += playerController.OnMoveInput;
        _inputs.Player.Move.canceled += playerController.OnMoveInput;

        _inputs.Player.Look.performed += playerController.OnLookInput;
        _inputs.Player.Look.canceled += playerController.OnLookInput;

        _inputs.Player.Jump.performed += playerController.OnJumpInput;
        _inputs.Player.Jump.canceled += playerController.OnJumpInput;
    }

    public void RemoveInputs(Player playerController)
    {
        _inputs.Player.Move.performed -= playerController.OnMoveInput;
        _inputs.Player.Move.canceled -= playerController.OnMoveInput;

        _inputs.Player.Look.performed -= playerController.OnLookInput;
        _inputs.Player.Look.canceled -= playerController.OnLookInput;

        _inputs.Player.Jump.performed -= playerController.OnJumpInput;
        _inputs.Player.Jump.canceled -= playerController.OnJumpInput;
    }
}