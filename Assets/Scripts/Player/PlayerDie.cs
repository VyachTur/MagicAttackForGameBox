using UnityEngine;
using StaticData;
using System;

namespace Player
{
    public class PlayerDie : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        public event Action OnPlayerDieEvent;

        private bool _isPlayerDie;
        public bool IsPlayerDie => _isPlayerDie;

        public void Die()
        {
            _isPlayerDie = true;
            _characterController.enabled = false;

            _animator.SetBool(Constants.IsPlayerAttack, false);
            _animator.SetBool(Constants.IsMove, false);
            _animator.SetTrigger(Constants.Die);

            GetComponent<PlayerScriptsDeactivate>().DisabledScripts(false);
            GetComponent<Collider>().enabled = false;
            GetComponent<PlayerHealth>().enabled = false;

            OnPlayerDieEvent?.Invoke();
        }
    }
}

