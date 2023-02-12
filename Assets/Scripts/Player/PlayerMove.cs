using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _gravity = Physics.gravity.y;

        [SerializeField] private Animator _playerAnimator;

        private CharacterController _characterController;
        private float _verticalInput;
        private float _horizontalInput;
        private Vector3 _dir;

        private void Start()
        {
            TryGetComponent(out _characterController);
        }

        private void Update()
        {
            _verticalInput = Input.GetAxis("Vertical");
            _horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            _dir = new Vector3(_horizontalInput * _moveSpeed, _gravity, _verticalInput * _moveSpeed) * Time.fixedDeltaTime;

            _characterController.Move(_dir); // player move fith gravity

            // Debug.DrawRay(transform.position, new Vector3(_dir.x, 0f, _dir.z) * 10f, Color.red);

        }
    }
}


