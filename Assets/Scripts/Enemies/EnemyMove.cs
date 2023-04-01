using UnityEngine;
using UnityEngine.AI;
using Player;
using StaticData;

namespace Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(EnemyDie))]
    public class EnemyMove : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        private PlayerHealth _target;
        private EnemyDie _enemyDie;

        private void OnEnable()
        {
            _target = FindObjectOfType<PlayerHealth>();
            _enemyDie = GetComponent<EnemyDie>();
            _agent.enabled = true;
        }

        private void Update()
        {
            if (_enemyDie.IsEnemyDeath)
            {
                _agent.enabled = false;
                return;
            }

            if (_target != null)
            {
                if (_target.Health <= 0f) _agent.enabled = false;
                if (_agent.enabled) _agent.SetDestination(_target.transform.position);

                _animator.SetFloat(Constants.Speed, _agent.velocity.magnitude);
            }
        }
    }
}

