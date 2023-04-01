using System;
using UnityEngine;

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

            _inputActions.PlayerControls.AttackFirst.performed += context => OnAttackFirstStart();
            _inputActions.PlayerControls.AttackFirst.canceled += context => OnAttackFirstEnd();
            _inputActions.PlayerControls.AttackSecond.performed += context => OnAttackSecondStart();
            _inputActions.PlayerControls.AttackSecond.canceled += context => OnAttackSecondEnd();
            _inputActions.PlayerControls.Move.performed += context => OnMoveStart();
            _inputActions.PlayerControls.Move.canceled += context => OnMoveEnd();
            _inputActions.PlayerControls.Look.performed += context => OnLook();
            _inputActions.PlayerControls.Dash.performed += context => OnDash();
        }

        private void OnDisable()
        {
            _inputActions.PlayerControls.AttackFirst.performed -= context => OnAttackFirstStart();
            _inputActions.PlayerControls.AttackFirst.canceled -= context => OnAttackFirstEnd();
            _inputActions.PlayerControls.AttackSecond.performed -= context => OnAttackSecondStart();
            _inputActions.PlayerControls.AttackSecond.canceled -= context => OnAttackSecondEnd();
            _inputActions.PlayerControls.Move.performed -= context => OnMoveStart();
            _inputActions.PlayerControls.Move.canceled -= context => OnMoveEnd();
            _inputActions.PlayerControls.Look.performed -= context => OnLook();
            _inputActions.PlayerControls.Dash.performed -= context => OnDash();

            _inputActions.Disable();
        }

        private void OnAttackFirstStart() => 
            OnPlayerFirstAttackStartEvent?.Invoke();

        private void OnAttackFirstEnd() => 
            OnPlayerFirstAttackEndEvent?.Invoke();

        private void OnAttackSecondStart()
        {
            OnPlayerSecondAttackStartEvent?.Invoke();
        }

        private void OnAttackSecondEnd()
        {
            OnPlayerSecondAttackEndEvent?.Invoke();
        }

        private void OnMoveStart() => 
            OnPlayerMoveStartEvent?.Invoke(_inputActions.PlayerControls.Move.ReadValue<Vector2>());

        private void OnMoveEnd() => 
            OnPlayerMoveEndEvent?.Invoke(_inputActions.PlayerControls.Move.ReadValue<Vector2>());

        private void OnLook() => 
            OnPlayerLookEvent?.Invoke(_inputActions.PlayerControls.Look.ReadValue<Vector2>());


        private void OnDash() =>
            OnPlayerDashEvent?.Invoke();
    }
}

