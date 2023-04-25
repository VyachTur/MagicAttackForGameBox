using System;
using UnityEngine;
using UnityEngine.Events;
using Enemies;
using Enemies.RedDragon;
using BaseCode;
using System.Collections;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _currentHealth;
        [SerializeField] private UnityEvent _damageEffectEvent;
        [SerializeField] private ParticleSystem _fireDamageEffect;
        [SerializeField] private TextPopup _textPopup;
        [SerializeField] private PlayerHealthAttraction _playerHealthAttraction;
        [SerializeField] private PlayerLevel _playerLevel;
        public event Action OnChangePlayerHealthEvent;

        public float MaxHealth => _maxHealth;
        public float Health => _currentHealth;

        private void Awake()
        {
            EnemyAttack.OnEnemyMakeDamageEvent += OnTakeDamage;
            RedDragonAttack.OnEnemyMakeDamageEvent += OnTakeFireDamage;

            _playerHealthAttraction.OnPlayerHealthAttractionEvent += OnTakeHeal;
            _playerLevel._OnLevelUpEvent += OnMakeFullHealth;
        }

        private void Start() => 
            SetScalerHP();

        private void SetScalerHP()
        {
            OnChangePlayerHealthEvent?.Invoke();
        }

        private void OnTakeDamage(float damage)
        {
            if (_currentHealth >= damage) _currentHealth -= damage;
            else _currentHealth = 0f;

            SetScalerHP();
            _textPopup.MakePopupText((-damage).ToString(), PopupTextType.DamagePopupText);

            if (_currentHealth <= 0f)
            {
                GetComponent<PlayerDie>()?.Die();
                return;
            }

            _damageEffectEvent?.Invoke();
        }

        private void OnTakeFireDamage(float damage)
        {
            _fireDamageEffect?.Play();

            StartCoroutine(ThreeFireDamage(damage));
        }

        private IEnumerator ThreeFireDamage(float damage)
        {
            for (int i = 0; i < 3; i++)
            {
                if (_currentHealth <= 0f)
                {
                    GetComponent<PlayerDie>()?.Die();
                    yield return null;
                }
                else
                {
                    OnTakeDamage(Mathf.Ceil(damage / 5f));
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        private void OnTakeHeal(float heal)
        {
            float addedHealth;

            if (_currentHealth + heal > _maxHealth) addedHealth = _maxHealth - _currentHealth;
            else addedHealth = heal;

            _currentHealth += addedHealth;

            if (addedHealth == 0f) return;

            SetScalerHP();
            _textPopup.MakePopupText($"+{addedHealth}", PopupTextType.HealPopupText);
        }

        private void OnMakeFullHealth()
        {
            _currentHealth = _maxHealth;
            SetScalerHP();
        }

        private void OnDestroy()
        {
            EnemyAttack.OnEnemyMakeDamageEvent -= OnTakeDamage;
            RedDragonAttack.OnEnemyMakeDamageEvent -= OnTakeFireDamage;
            _playerHealthAttraction.OnPlayerHealthAttractionEvent -= OnTakeHeal;
        }
    }
}
