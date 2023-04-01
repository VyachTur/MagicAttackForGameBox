using UnityEngine;
using StaticData;

namespace Player
{
    public class PlayerWin : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterController _characterController;

        private bool _isPlayerWin;
        public bool IsPlayerWin => _isPlayerWin;

        public void Win()
        {
            _isPlayerWin = true;
            _characterController.enabled = false;

            _animator.SetBool(Constants.IsPlayerAttack, false);
            _animator.SetBool(Constants.IsMove, false);

            GetComponent<PlayerScriptsDeactivate>().DisabledScripts(false);
            GetComponent<Collider>().enabled = false;
            GetComponent<PlayerHealth>().enabled = false;
        }
    }
}

