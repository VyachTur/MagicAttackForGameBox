using UnityEngine;
using Player.Input;
using StaticData;
using Effects;
using UnityEngine.EventSystems;
using System.Collections.Generic;

using Mouse = UnityEngine.InputSystem.Mouse;

namespace Player
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private AreaEffectInstantiate _areaEffectInstantiate;

        private IInputAttack _inputAttack;

        private bool _isFirstAttackDo;

        private void Awake()
        {
            _inputAttack = GetComponent<PlayerInput>();

            _inputAttack.OnPlayerFirstAttackStartEvent += AttackFirstStart;
            _inputAttack.OnPlayerFirstAttackEndEvent += AttackFirstEnd;
            _inputAttack.OnPlayerSecondAttackStartEvent += AttackSecondStart;
            _inputAttack.OnPlayerSecondAttackEndEvent += AttackSecondEnd;
        }

        private void AttackFirstStart()
        {
            // если действие происходит над элементом UI, то выходим из метода
            if (IsClickOnUI()) return;

            _animator.SetBool(Constants.IsPlayerAttack, true);
        }

        private void AttackFirstEnd()
        {
            // если действие происходит над элементом UI, то выходим из метода
            if (IsClickOnUI()) return;

            _animator.SetBool(Constants.IsPlayerAttack, false);
        }

        private bool IsClickOnUI()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            List<RaycastResult> raycastResultsList = new List<RaycastResult>();
            pointerEventData.position = Mouse.current.position.ReadValue();

            EventSystem.current.RaycastAll(pointerEventData, raycastResultsList);

            return raycastResultsList.Count > 0;
        }

        private void AttackSecondStart() =>
            _areaEffectInstantiate.BeginEffect();

        private void AttackSecondEnd() =>
            _areaEffectInstantiate.EndEffect();

        private void OnDestroy()
        {
            _inputAttack.OnPlayerFirstAttackStartEvent -= AttackFirstStart;
            _inputAttack.OnPlayerFirstAttackEndEvent -= AttackFirstEnd;
            _inputAttack.OnPlayerSecondAttackStartEvent -= AttackSecondStart;
            _inputAttack.OnPlayerSecondAttackEndEvent -= AttackSecondEnd;
        }
    }
}

