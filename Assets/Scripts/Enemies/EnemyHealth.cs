using UnityEngine;
using BaseCode;
using StaticData;

namespace Enemies
{
    [RequireComponent(typeof(EnemyDie))]
    public class EnemyHealth : MonoBehaviour, IDamagable
    {
        [SerializeField] private EnemySO _enemySO;
        [SerializeField] private float _currentHealth;
        [SerializeField] private GameObject _scalerHP;
        [SerializeField] private TextPopup _textPopup;

        private EnemyDie _enemyDie;

        private void OnEnable()
        {
            _enemyDie = GetComponent<EnemyDie>();
            _currentHealth = _enemySO.MaxHealth;
            SetScalerHP();
        }

        private void SetScalerHP()
        {
            float health = (float)_currentHealth / _enemySO.MaxHealth;
            _scalerHP.transform.localScale = new Vector3(health, 1f, 1f);
        }

        public void TakeDamage(float damage)
        {
            if (_enemyDie.IsEnemyDeath) return;

            if (_currentHealth >= damage) _currentHealth -= damage;
            else _currentHealth = 0f;

            SetScalerHP();
            _textPopup.MakePopupText((-damage).ToString(), PopupTextType.DamagePopupText);

            if (_currentHealth == 0f)
            {
                _enemyDie.Die();
            }
        }
    }

}
