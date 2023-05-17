using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Input
{
    public class PlayerInput : MonoBehaviour, IInputAttack, IInputMove, IInputLook, IInputDash
    {
        public event Action OnPlayerFirstAttackStartEvent;
        public event Action OnPlayerFirstAttackEndEvent;
        public event Action OnPlayerSecondAttackStartEvent;
        public event Action OnPlayerSecondAttackEndEvent;
        public event Action OnPlayerDashEvent;
        public event Action<Vector2> OnPlayerMoveStartEvent;
        public event Action<Vector2> OnPlayerMoveEndEvent;

        public event Action<Vector2> OnPlayerLookEvent;

        private PlayerInputActions _inputActions;

        private void Awake()
        {
            _inputActions = new PlayerInputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.PlayerControls.AttackFirst.performed += OnAttackFirstStart;
            _inputActions.PlayerControls.AttackFirst.canceled += OnAttackFirstEnd;
            _inputActions.PlayerControls.AttackSecond.performed += OnAttackSecondStart;
            _inputActions.PlayerControls.AttackSecond.canceled += OnAttackSecondEnd;
            _inputActions.PlayerControls.Move.performed += OnMoveStart;
            _inputActions.PlayerControls.Move.canceled += OnMoveEnd;
            _inputActions.PlayerControls.Look.performed += OnLook;
            _inputActions.PlayerControls.Dash.performed += OnDash;
        }

        private void OnDisable()
        {
            _inputActions.PlayerControls.AttackFirst.performed -= OnAttackFirstStart;
            _inputActions.PlayerControls.AttackFirst.canceled -= OnAttackFirstEnd;
            _inputActions.PlayerControls.AttackSecond.performed -= OnAttackSecondStart;
            _inputActions.PlayerControls.AttackSecond.canceled -= OnAttackSecondEnd;
            _inputActions.PlayerControls.Move.performed -= OnMoveStart;
            _inputActions.PlayerControls.Move.canceled -= OnMoveEnd;
            _inputActions.PlayerControls.Look.performed -= OnLook;
            _inputActions.PlayerControls.Dash.performed -= OnDash;

            _inputActions.Disable();
        }

        private void OnAttackFirstStart(InputAction.CallbackContext context) => 
            OnPlayerFirstAttackStartEvent?.Invoke();

        private void OnAttackFirstEnd(InputAction.CallbackContext context) => 
            OnPlayerFirstAttackEndEvent?.Invoke();

        private void OnAttackSecondStart(InputAction.CallbackContext context)
        {
            OnPlayerSecondAttackStartEvent?.Invoke();
        }

        private void OnAttackSecondEnd(InputAction.CallbackContext context)
        {
            OnPlayerSecondAttackEndEvent?.Invoke();
        }

        private void OnMoveStart(InputAction.CallbackContext context) => 
            OnPlayerMoveStartEvent?.Invoke(_inputActions.PlayerControls.Move.ReadValue<Vector2>());

        private void OnMoveEnd(InputAction.CallbackContext context) => 
            OnPlayerMoveEndEvent?.Invoke(_inputActions.PlayerControls.Move.ReadValue<Vector2>());

        private void OnLook(InputAction.CallbackContext context) => 
            OnPlayerLookEvent?.Invoke(_inputActions.PlayerControls.Look.ReadValue<Vector2>());


        private void OnDash(InputAction.CallbackContext context) =>
            OnPlayerDashEvent?.Invoke();
    }
}

