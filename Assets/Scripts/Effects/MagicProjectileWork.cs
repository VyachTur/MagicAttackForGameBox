using BaseCode;
using UnityEngine;
using StaticData;

namespace Effects
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class MagicProjectileWork : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _flashEffect;

        public MagicProjectileSO MageicProjectileSO;

        private Rigidbody _effectRigidbody;
        private Collider _effectCollider;

        private void Start()
        {
            _effectCollider = GetComponent<Collider>();
            _effectRigidbody = GetComponent<Rigidbody>();

            _effectRigidbody.velocity = transform.forward * MageicProjectileSO.FlySpeed;
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject, 0.3f);
            _flashEffect.Play();

            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.gameObject.GetComponent<IDamagable>().TakeDamage(MageicProjectileSO.Damage);
                _effectRigidbody.velocity = Vector3.zero;
                _effectCollider.enabled = false;
            }
        }
    }
}
