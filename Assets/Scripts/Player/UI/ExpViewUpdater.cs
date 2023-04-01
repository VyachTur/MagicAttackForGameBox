using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Player.UI
{
    public class ExpViewUpdater : MonoBehaviour
    {
        [SerializeField] private Slider _expSlider;
        [SerializeField] private TMP_Text _expValueText;
        [SerializeField] private PlayerExp _playerExp;
        [SerializeField] private PlayerLevel _playerLevel;

        private void Awake()
        {
            _playerExp.OnChangePlayerExpEvent += OnChangePlayerExp;
        }

        private void OnChangePlayerExp()
        {
            _expSlider.value = _playerExp.Exp/_playerExp.MaxExp;
            _expValueText.text = $"Опыт: {_playerExp.Exp}/{_playerExp.MaxExp} (Уровень {_playerLevel.CurrentLevel})";
        }

        private void OnDestroy()
        {
            _playerExp.OnChangePlayerExpEvent -= OnChangePlayerExp;
        }
    }
}

