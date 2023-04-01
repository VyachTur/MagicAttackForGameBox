using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using BaseCode.Menu;
using Player.AbilityInfo;

namespace Player
{
    public class PlayerLevel : MonoBehaviour
    {
        public event Action _OnLevelUpEvent;
        [SerializeField] private GameObject _levelUpCanvas;
        [SerializeField] private PlayerExp _playerExp;
        [SerializeField] private PlayerScriptsDeactivate _playerScriptDeactivate;
        [SerializeField] private int _maxLevel;
        [SerializeField] private int _currentLevel;
        [SerializeField] private ParticleSystem _levelUpEffect;
        [SerializeField] private PauseGame _pauseGame;

        public int MaxLevel => _maxLevel;
        public int CurrentLevel => _currentLevel;

        private void Awake() => 
            _playerExp.OnPlayerNextLevelEvent += OnLevelUp;

        private void Start()
        {
            MagicProjectileInfo.Reset();
            DashInfo.Reset();
            AreaAttackInfo.Reset();
        }

        private void OnLevelUp()
        {
            if (_currentLevel < _maxLevel)
                _currentLevel++;
            
            _OnLevelUpEvent?.Invoke();

            _levelUpEffect.Play();
            _playerScriptDeactivate.DisabledScripts(false);
            
            ShowCards().Forget();
        }

        private async UniTask ShowCards()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f), ignoreTimeScale: true);
            
            _pauseGame.PauseOn();
            _levelUpCanvas.SetActive(true);
        }

        private void OnDestroy()
        {
            _playerExp.OnPlayerNextLevelEvent -= OnLevelUp;
        }
    }
}

