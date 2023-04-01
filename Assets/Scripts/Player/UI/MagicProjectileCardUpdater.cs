using UnityEngine;
using TMPro;
using Player.AbilityInfo;
using System;
using UnityEngine.UI;

namespace Player.UI
{
    public class MagicProjectileCardUpdater : MonoBehaviour
    {
        public event Action<float> OnMagicProjectileLevelUpEvent;

        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private TMP_Text _damageText;
        [SerializeField] private Button _cardButton;

        private float _damageAdd;

        private void Start()
        {
            MagicProjectileInfo.Reset();
        }

        private void OnEnable()
        {
            if (MagicProjectileInfo.MaxLevel <= MagicProjectileInfo.LevelNumber)
            {
                _cardButton.interactable = false;
                return;
            }

            _damageAdd = Mathf.Ceil(((MagicProjectileInfo.LevelNumber + 1) / 3f) * 1.3f);

            UpdateData(MagicProjectileInfo.LevelNumber + 1f, MagicProjectileInfo.Damage + _damageAdd);
        }

        private void UpdateData(float level, float damage)
        {
            _levelNumberText.text = $"Уровень {level}";
            _damageText.text = $"Урон: {damage}";
        }

        public void LevelUp()
        {
            MagicProjectileInfo.LevelNumber += 1;
            MagicProjectileInfo.Damage += _damageAdd;
            
            UpdateData(MagicProjectileInfo.LevelNumber, MagicProjectileInfo.Damage);

            OnMagicProjectileLevelUpEvent?.Invoke(MagicProjectileInfo.Damage);
        }
    }
}

