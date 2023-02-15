using UnityEngine;
using StaticData;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _gravity = Physics.gravity.y;
        [SerializeField] private Animator _playerAnimator;

        private CharacterController _characterController;
        private IInputMove _inputMove;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _inputMove = FindObjectOfType<PlayerInput>();
            _inputMove.OnPlayerMoveEvent += SetMoveDirection;
        }

        private void SetMoveDirection(Vector2 direction)
        {
            _moveDirection = direction;
        }

        private void Start()
        {
            TryGetComponent(out _characterController);
        }

        private void FixedUpdate()
        {
            Vector3 moveToward = new Vector3(_moveDirection.x * _moveSpeed, _gravity, _moveDirection.y * _moveSpeed) * Time.fixedDeltaTime;

            _characterController.Move(moveToward); // player move fith gravity

            MoveAnimation();

            // Debug.DrawRay(transform.position, new Vector3(_moveDirection.x, 0f, _moveDirection.z) * 10f, Color.red);
        }

        private bool IsPlayerMove()
        {
            return (_moveDirection.x > Constants.Epsilon || _moveDirection.y > Constants.Epsilon
                    || _moveDirection.x < -Constants.Epsilon || _moveDirection.y < -Constants.Epsilon);
        }

        private void MoveAnimation()
        {
            if (IsPlayerMove())
            {
                _playerAnimator.SetBool(Constants.IsMove, true);
                _playerAnimator.SetFloat(Constants.MoveX, GetAxisXToAnimation(transform.forward, _moveDirection));
                _playerAnimator.SetFloat(Constants.MoveY, GetAxisYToAnimation(transform.forward, _moveDirection));
            }
            else
            {
                _playerAnimator.SetBool(Constants.IsMove, false);
            }
        }


        private float GetAxisXToAnimation(Vector3 forward, Vector3 moveDirection)
        {
            if (moveDirection.x == 1f) return forward.z;
            if (moveDirection.x == -1f) return -forward.z;
            if (moveDirection.y == 1f) return -forward.x;
            if (moveDirection.y == -1f) return forward.x;

            return 0f;
        }

        private float GetAxisYToAnimation(Vector3 forward, Vector3 moveDirection)
        {
            if (moveDirection.x == 1f) return forward.x;
            if (moveDirection.x == -1f) return -forward.x;
            if (moveDirection.y == 1f) return forward.z;
            if (moveDirection.y == -1f) return -forward.z;

            return 0f;
        }
    }
}


