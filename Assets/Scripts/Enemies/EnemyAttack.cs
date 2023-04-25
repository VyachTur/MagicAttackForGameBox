using UnityEngine;
using UnityEngine.AI;
using Player;
using StaticData;
using System;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Animator _enemyAnimator;
        [SerializeField] private EnemySO _enemySO;
        [SerializeField] private LayerMask _layerMaskDamagableObject;
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
                float radiusToPlayer = Vector3.Distance(transform.position, _target.transform.position);

                if (radiusToPlayer <= _enemySO.AttackDistance)
                {
                    _enemyAnimator.SetTrigger(Constants.Attack);
                }
            } 
            else 
                _enemyAnimator.ResetTrigger(Constants.Attack);
        }

        public virtual void MakeDamage()
        {
            Vector3 sphereStart = transform.position + new Vector3(0f, _enemySO.OffsetY, 0f);

            if (Physics.SphereCast(sphereStart, 
                                    _enemySO.EnemyCastSphereForAttack, 
                                    transform.forward, 
                                    out _, 
                                    _enemySO.AttackDistance, 
                                    _layerMaskDamagableObject))
            {
                OnEnemyMakeDamageEvent?.Invoke(_enemySO.Damage);
            }
        }


#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0f, _enemySO.OffsetY, _enemySO.AttackDistance), _enemySO.EnemyCastSphereForAttack);
        }

#endif
    }
}