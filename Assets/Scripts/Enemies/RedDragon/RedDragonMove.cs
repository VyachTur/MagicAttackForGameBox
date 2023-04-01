using UnityEngine;
using UnityEngine.AI;
using Player;
using StaticData;

namespace Enemies.RedDragon
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(RedDragonDie))]
    public class RedDragonMove : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private RedDragonSO _redDragonSO;

        [SerializeField] private float _idleAgentSpeed = 0f;
        [SerializeField] private float _walkAgentSpeed = 2.4f;
        [SerializeField] private float _runAgentSpeed = 4.4f;

        [SerializeField] private Animator _animator;
        private PlayerHealth _playerHealth;
        private RedDragonDie _redDragonDie;

        private void OnEnable()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _redDragonDie = GetComponent<RedDragonDie>();
            _agent.enabled = true;
            _agent.speed = _idleAgentSpeed;
        }

        private void Update()
        {
            if (_playerHealth?.Health <= 0f)
            {
                _agent.speed = _idleAgentSpeed;
                return;
            }

            if (_redDragonDie.IsEnemyDeath)
            {
                _agent.enabled = false;
                return;
            }

            if (_playerHealth != null && _playerHealth?.Health > 0f)
            {
                Vector3 playerPosition = _playerHealth.transform.position;
                float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

                if (_playerHealth.Health <= 0f) _agent.enabled = false;
                if (_agent.enabled) _agent.SetDestination(playerPosition);

                AnimatorStateInfo animatorState = _animator.GetCurrentAnimatorStateInfo(0);

                if (animatorState.IsName("Idle") || animatorState.IsName("Walk") || animatorState.IsName("Run"))
                {
                    if (distanceToPlayer <= _redDragonSO.AttackDistance)
                        _agent.speed = _idleAgentSpeed;

                    if (distanceToPlayer > _redDragonSO.AttackDistance && distanceToPlayer <= _redDragonSO.WalkDistance)
                        _agent.speed = _walkAgentSpeed;
                    else
                        _agent.speed = _runAgentSpeed;
                }
                else
                {
                    if (animatorState.IsName("Take Off") || animatorState.IsName("Fly Flame Attack") || animatorState.IsName("Land"))
                        _agent.speed = _walkAgentSpeed;
                    else
                        _agent.speed = _idleAgentSpeed;
                }

                _animator.SetFloat(Constants.Speed, _agent.speed);
            }
        }
    }
}

