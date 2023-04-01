using UnityEngine;
using UnityEngine.AI;

namespace Enemies.RedDragon
{
    public class RedDragonStart : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _agentSpeed = 2.2f;

        private void Start()
        {
            _agent.speed = 0f;

            Invoke("AgentOnFromFiveSectonds", 10f);
        }

        private void AgentOnFromFiveSectonds()
        {
            // print("Speed UP!");
            _agent.speed = _agentSpeed;
        }

    }
}