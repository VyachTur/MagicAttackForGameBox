using UnityEngine;
using Player.Input;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
using Player.AbilityInfo;
using Player.UI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(CharacterController))]
    public class PlayerDash : MonoBehaviour
    {
        [SerializeField] private float _dashPower = DashInfo.DashPower;
        [SerializeField] private ParticleSystem _dashEffect;
        [SerializeField] private AudioSource _dashSound;
        [SerializeField] private Image _dashImage;
        [SerializeField] private float _timeToDashReload = DashInfo.ReloadTime;
        [SerializeField] private DashCardUpdater _dashCardUpdater;
        [SerializeField] private bool _isActiveDash;

        public event Action OnDashReloadEvent;
        public float TimeToDashReload => _timeToDashReload;

        private CharacterController _characterController;
        private bool _isDashReload;
        private const float CountStepsToDash = 0.2f;

        private IInputDash _inputDash;
        private Collider _collider;

        private void Awake()
        {
            _inputDash = GetComponent<PlayerInput>();
            _characterController = GetComponent<CharacterController>();

            _inputDash.OnPlayerDashEvent += Dash;
            _dashCardUpdater.OnDashLevelUpEvent += OnSetDashData;

            _collider = GetComponent<Collider>();
        }

        private void OnSetDashData(float dashPower, float dashReloadTime)
        {
            _isActiveDash = true;
            _dashImage.enabled = true;
            _dashPower = dashPower;
            _timeToDashReload = dashReloadTime;
        }

        private void Dash()
        {
            if (_isActiveDash && !_isDashReload)
            {
                MakeDash().Forget();
                DashReload().Forget();
            }
        }

        private async UniTask MakeDash()
        {
            LayerMask startLayer = gameObject.layer;
            gameObject.layer = LayerMask.NameToLayer("PlayerImpervious");
            _dashEffect.Play();
            _dashSound.Play();

            for (float step = 0; step < CountStepsToDash; step += Time.deltaTime)
            {
                _characterController.SimpleMove(transform.forward * _dashPower);
                await UniTask.Yield();
            }

            gameObject.layer = startLayer;
        }
        
        private async UniTask DashReload() 
        {
            OnDashReloadEvent?.Invoke();

            _isDashReload = true;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeToDashReload));
            _isDashReload = false;
        }

        private void OnDestroy()
        {
            _inputDash.OnPlayerDashEvent -= Dash;
            _dashCardUpdater.OnDashLevelUpEvent -= OnSetDashData;
        }
    }
}

