using UnityEngine;
using BaseCode;
using StaticData;
using UnityEngine.UI;

namespace Enemies.RedDragon
{
    [RequireComponent(typeof(RedDragonDie))]
    public class RedDragonHealth : MonoBehaviour, IDamagable
    {
        [SerializeField] private EnemySO _enemySO;
        [SerializeField] private float _currentHealth;
        [SerializeField] private Slider _sliderHP;
        [SerializeField] private TextPopup _textPopup;

        private RedDragonDie _dragonDie;

        private void OnEnable()
        {
            _dragonDie = GetComponent<RedDragonDie>();
            _currentHealth = _enemySO.MaxHealth;
            SetScalerHP();
        }

        private void SetScalerHP()
        {
            _sliderHP.value = (float)_currentHealth / _enemySO.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (_dragonDie.IsEnemyDeath) return;

            if (_currentHealth >= damage) _currentHealth -= damage;
            else _currentHealth = 0f;

            SetScalerHP();
            _textPopup.MakePopupText((-damage).ToString(), PopupTextType.DamagePopupText);

            if (_currentHealth == 0f)
            {
                _dragonDie.Die();
            }
        }
    }
}
