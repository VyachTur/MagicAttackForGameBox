using System.Collections;
using System.Linq;
using BaseCode;
using Enemies;
using StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace Effects
{
    public class AreaEffectDamage : MonoBehaviour
    {
        [SerializeField] private ArreaAttackSO _arreaAttackSO;
        [SerializeField] private AudioSource _audioEffect;
        
        public float AreaAttackDamage;

        private LayerMask _enemyLayerMask;

        private void Awake()
        {
            AreaAttackDamage = _arreaAttackSO.Damage;
        }

        private void Start()
        {
            _enemyLayerMask = 1 << LayerMask.NameToLayer("Enemy");

            Invoke("AreaDamageToEnemies", _arreaAttackSO.DamageActivateAfterSeconds);
        }

        private void AreaDamageToEnemies()
        {
            var damagables = Physics.OverlapSphere(transform.position, _arreaAttackSO.DamageMakeRadius, _enemyLayerMask)
                                                .Select(collider => new
                                                {
                                                    Body = collider.GetComponent<Rigidbody>(),
                                                    Damage = collider.GetComponent<IDamagable>(),
                                                    Death = collider.GetComponent<EnemyDie>()
                                                });

            _audioEffect.Play();

            foreach (var damagable in damagables)
            {
                damagable.Damage.TakeDamage(AreaAttackDamage);

                if (damagable.Death.IsEnemyDeath) continue;

                StartCoroutine(MakeExplosionForce(damagable.Body, false, 0f));

                if (_arreaAttackSO.EffectType == EffectType.NatureAreaEffect)
                    StartCoroutine(MakeExplosionForce(damagable.Body, true, 1.4f));
                else
                    StartCoroutine(MakeExplosionForce(damagable.Body, true, 0.5f));

                if (_arreaAttackSO.EffectType == EffectType.FireAreaEffect)
                    StartCoroutine(MakeFire(damagable.Body.transform, damagable.Damage));
            }
        }

        private IEnumerator MakeExplosionForce(Rigidbody rigidbody, bool canOnAgentAndKinematic, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);

            rigidbody.isKinematic = canOnAgentAndKinematic;
            rigidbody.useGravity = !canOnAgentAndKinematic;
            if (rigidbody.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
            {
                agent.enabled = canOnAgentAndKinematic;
            }

            rigidbody.AddExplosionForce(_arreaAttackSO.Power, transform.position, _arreaAttackSO.DamageMakeRadius, _arreaAttackSO.UpForce, ForceMode.Impulse);
        }

        private IEnumerator MakeFire(Transform transform, IDamagable damagable)
        {
            ParticleSystem fireEffect = transform.GetComponentsInChildren<ParticleSystem>()
                                                    .Where(effect => effect.tag == "Fire")
                                                    .FirstOrDefault();

            if (fireEffect != null && fireEffect.tag == "Fire")
                fireEffect.Play();
            else
                yield return null;

            for (int i = 0; i < 3; i++)
            {
                damagable.TakeDamage(Mathf.Floor(AreaAttackDamage / 2));
                yield return new WaitForSeconds(0.9f);
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _arreaAttackSO.DamageMakeRadius);
        }

#endif

    }
}

