using UnityEngine;
using Effects;
using BaseCode;
using Player.UI;
using Player.AbilityInfo;

namespace Player
{
    public class MakeDamageFromLayerMask : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _attackDistance;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private GameObject _magicProjectileLight;
        [SerializeField] private Transform _magicProjectileSpawner;
        [SerializeField] private MagicProjectileCardUpdater _magicProjectleCard;
        [SerializeField] private MagicProjectileWork _magicProjectilePrefab;
        [SerializeField] private AudioSource _magicProjectileSound;
        [SerializeField] private bool _isActiveMagicProjectile;
        [SerializeField] private float _damageMultiplier = MagicProjectileInfo.Damage;
        private const float OffsetY = 0.7f;

        private void Awake()
        {
            _magicProjectleCard.OnMagicProjectileLevelUpEvent += OnSetDamageMultiplierFromCard;
        }

        private void OnSetDamageMultiplierFromCard(float damage)
        {
            _isActiveMagicProjectile = true;
            _damageMultiplier = damage;
        }

        public void MakeDamage()
        {
            if (_isActiveMagicProjectile)
            {
                MagicProjectileWork magicProjectile = Instantiate(_magicProjectilePrefab, _magicProjectileSpawner.position, _magicProjectileSpawner.rotation);
                _magicProjectileSound.Play();
                magicProjectile.MageicProjectileSO.Damage = _damage * _damageMultiplier;

                return;
            }

            Vector3 rayStart = transform.position + new Vector3(0f, OffsetY, 0f);
            if(Physics.Raycast(rayStart, transform.forward, out RaycastHit hit, _attackDistance, _layerMask))
            {
                hit.transform.gameObject.GetComponent<IDamagable>().TakeDamage(_damage);
            }

            Debug.DrawRay(rayStart, transform.forward * _attackDistance, Color.yellow, 0.3f);
        }

        private void Update()
        {
            if (_isActiveMagicProjectile && !_magicProjectileLight.activeInHierarchy)
                _magicProjectileLight.SetActive(true);
        }

        private void OnDestroy()
        {
            _magicProjectleCard.OnMagicProjectileLevelUpEvent -= OnSetDamageMultiplierFromCard;
        }
    }
}

