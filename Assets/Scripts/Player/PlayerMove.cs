using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _gravity = Physics.gravity.y;

        [SerializeField] private Animator _playerAnimator;

        private CharacterController _characterController;
        // private float _verticalInput;
        // private float _horizontalInput;
        private IInputMove _inputMove;
        private Vector3 _direction;

        private void Awake()
        {
            _inputMove = FindObjectOfType<PlayerInput>();
            _inputMove.OnPlayerMoveEvent += OnSetInputDirection;
        }

        private void OnSetInputDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void Start()
        {
            TryGetComponent(out _characterController);
        }

        // private void Update()
        // {
        //     // _verticalInput = Input.GetAxis("Vertical");
        //     // _horizontalInput = Input.GetAxis("Horizontal");


        // }

        // public void OnMovement(InputAction.CallbackContext value)
        // {
        //     Vector2 inputMovement = value.ReadValue<Vector2>();
            
        //     _direction = new Vector3(inputMovement.x * _moveSpeed, _gravity, inputMovement.y * _moveSpeed);

        //     print("OnMovement");
        // }

        private void FixedUpdate()
        {
            _direction = new Vector3(_direction.x * _moveSpeed, _gravity, _direction.y * _moveSpeed) * Time.fixedDeltaTime;

            // _direction = _direction * Time.fixedDeltaTime;

            _characterController.Move(_direction); // player move fith gravity

            // Debug.DrawRay(transform.position, new Vector3(_direction.x, 0f, _direction.z) * 10f, Color.red);

        }
    }
}


