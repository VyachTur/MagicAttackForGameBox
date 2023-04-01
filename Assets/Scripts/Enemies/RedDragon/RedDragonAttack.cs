using UnityEngine;
using UnityEngine.AI;
using Player;
using StaticData;
using System;

using Random = UnityEngine.Random;

namespace Enemies.RedDragon
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class RedDragonAttack : MonoBehaviour
    {
        [SerializeField] private Animator _enemyAnimator;
        [SerializeField] private RedDragonSO _redDragonSO;
        [SerializeField] private LayerMask _layerMaskDamagableObject;
        [SerializeField] private ParticleSystem _redDragonAttackEffect;

        public static event Action<float> OnEnemyMakeDamageEvent;

        private PlayerHealth _target;

        private void OnEnable()
        {
            _target = FindObjectOfType<PlayerHealth>();
        }

        private void Update()
        {
            if (_target != null && _target.Health > 0f)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, _target.transform.position);

                if (distanceToPlayer <= _redDragonSO.AttackDistance)
                    MakeRandomAttack();
            }
        }

        private void MakeRandomAttack()
        {
            ResetTriggers();

            if (Random.Range(0, 3) == 0)
                _enemyAnimator.SetTrigger(Constants.DragonAttack2);
            else
                _enemyAnimator.SetTrigger(Constants.DragonAttack1);
        }

        private void ResetTriggers()
        {
            _enemyAnimator.ResetTrigger(Constants.DragonAttack1);
            _enemyAnimator.ResetTrigger(Constants.DragonAttack2);
        }

        public void MakeDamage()
        {
            _redDragonAttackEffect?.Play();
            Invoke("MakeDamageRepeater", 0.6f);
        }

        private void MakeDamageRepeater()
        {
            Vector3 sphereStart = transform.position + new Vector3(0f, _redDragonSO.OffsetY, 0f);

            if (Physics.SphereCast(sphereStart, _redDragonSO.EnemyCastSphereForAttack, transform.forward, out _, _redDragonSO.AttackDistance, _layerMaskDamagableObject))
            {
                OnEnemyMakeDamageEvent?.Invoke(_redDragonSO.Damage);
            }
        }


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0f, _redDragonSO.OffsetY, _redDragonSO.AttackDistance), _redDragonSO.EnemyCastSphereForAttack);
        }

#endif
    }
}