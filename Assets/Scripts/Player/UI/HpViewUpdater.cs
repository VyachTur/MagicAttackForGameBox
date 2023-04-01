using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Player.UI
{
    public class HpViewUpdater : MonoBehaviour
    {
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private TMP_Text _hpValueText;
        [SerializeField] private PlayerHealth _playerHealth;

        private void Awake()
        {
            _playerHealth.OnChangePlayerHealthEvent += OnPlayerHealthChange;
        }

        private void OnPlayerHealthChange()
        {
            _hpSlider.value = _playerHealth.Health/_playerHealth.MaxHealth;
            _hpValueText.text = $"Здоровье: {_playerHealth.Health}/{_playerHealth.MaxHealth}";
        }

        private void OnDestroy()
        {
            _playerHealth.OnChangePlayerHealthEvent -= OnPlayerHealthChange;
        }
    }
}

