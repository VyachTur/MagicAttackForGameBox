using System;
using UnityEngine;
using BaseCode;

namespace Player
{
    public class PlayerExp : MonoBehaviour
    {
        [SerializeField] private float _maxExp;
        [SerializeField] private float _currentExp;
        [SerializeField] private PlayerExpAttraction _playerExpAttraction;
        [SerializeField] private PlayerLevel _playerLevel;
        [SerializeField] private TextPopup _textPopup;
        public event Action OnChangePlayerExpEvent;
        public event Action OnPlayerNextLevelEvent;

        public float MaxExp => _maxExp;
        public float Exp => _currentExp;

        private void Awake()
        {
            _playerExpAttraction.OnPlayerExpAttractionEvent += OnTakeExp;
        }

        private void Start()
        {
            SetScalerExp();
        }

        private void OnTakeExp(float exp)
        {
            if (_currentExp + exp >= _maxExp)
            {
                if (_playerLevel.CurrentLevel == _playerLevel.MaxLevel)
                {
                    _currentExp = _maxExp;
                    SetScalerExp();
                    return;
                }

                _currentExp = _currentExp + exp - _maxExp;
                _maxExp *= 1.2f;
                _maxExp = Mathf.Ceil(_maxExp);
                
                SetScalerExp();
                _textPopup.MakePopupText($"+{exp}", PopupTextType.ExpPopupText);

                OnPlayerNextLevelEvent?.Invoke();
            }
            else _currentExp += exp;

            SetScalerExp();
            _textPopup.MakePopupText($"+{exp}", PopupTextType.ExpPopupText);
        }

        private void SetScalerExp()
        {
            OnChangePlayerExpEvent?.Invoke();
        }

        private void OnDestroy()
        {
            _playerExpAttraction.OnPlayerExpAttractionEvent -= OnTakeExp;
        }
    }
}

