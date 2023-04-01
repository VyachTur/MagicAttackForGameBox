using UnityEngine;
using TMPro;
using Player.AbilityInfo;
using System;
using UnityEngine.UI;

namespace Player.UI
{
    public class DashCardUpdater : MonoBehaviour
    {
        public event Action<float, float> OnDashLevelUpEvent;

        [SerializeField] private TMP_Text _levelNumberText;
        [SerializeField] private TMP_Text _dashPowerText;
        [SerializeField] private TMP_Text _reloadTimeText;
        [SerializeField] private Button _cardButton;

        private void Start()
        {
            DashInfo.Reset();
        }

        private void OnEnable()
        {
            if (DashInfo.MaxLevel <= DashInfo.LevelNumber)
            {
                _cardButton.interactable = false;
                return;
            }

            if (DashInfo.ReloadTime == 1)
                UpdateData(DashInfo.LevelNumber + 1f, DashInfo.DashPower + 4f, DashInfo.ReloadTime);
            else
                UpdateData(DashInfo.LevelNumber + 1f, DashInfo.DashPower + 4f, DashInfo.ReloadTime - 1f);
        }

        private void UpdateData(float level, float power, float time)
        {
            _levelNumberText.text = $"Уровень {level}";
            _dashPowerText.text = $"Сила рывка: {power}";
            _reloadTimeText.text = $"Перезарядка: {time} сек";
        }

        public void LevelUp()
        {
            DashInfo.LevelNumber += 1;
            DashInfo.DashPower += 4f;
            
            if (DashInfo.ReloadTime > 1f) DashInfo.ReloadTime -= 1f;
            
            UpdateData(DashInfo.LevelNumber, DashInfo.DashPower, DashInfo.ReloadTime);
            
            OnDashLevelUpEvent?.Invoke(DashInfo.DashPower, DashInfo.ReloadTime);
        }
    }
}

