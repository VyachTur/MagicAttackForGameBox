using UnityEngine;
using StaticData;
using System;
using Environments;

namespace Enemies
{
    [RequireComponent(typeof(EnemyMove))]
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(EnemyLookAtPlayer))]
    public class EnemyDie : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _enemyRendererObject;
        [SerializeField] private EnemyLootVariants _loot;
        [SerializeField] private EnemySO _enemySO;
 
        public static event Action<int> OnEnemyMakeScoreEvent;
        public bool IsEnemyDeath { get; private set; }

        private EnemyMove _enemyMove;
        private Collider _collider;
        private EnemyLookAtPlayer _lookAtPlayer;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _lookAtPlayer = GetComponent<EnemyLookAtPlayer>();
        }

        private void OnEnable()
        {
            IsEnemyDeath = false;
            _enemyMove = GetComponent<EnemyMove>();
            _collider.enabled = true;
            _lookAtPlayer.enabled = true;
        }

        public void Die()
        {
            IsEnemyDeath = true;
            _animator.SetTrigger(Constants.Die);
            _collider.enabled = false;
            _lookAtPlayer.enabled = false;
            OnEnemyMakeScoreEvent?.Invoke(_enemySO.ScoreForPlayer);

            Invoke("DeactivateObject", 3f);
        }

        private void DeactivateObject()
        {
            gameObject.SetActive(false);

            GameObject randomLoot = _loot.GetRandomLoot();
            if (randomLoot != null)
            {
                Loot loot = Instantiate(randomLoot, transform.position, Quaternion.identity).GetComponent<Loot>();
                
                if (loot is PlusExp) loot.LootValue = _enemySO.ExpForPlayerLoot;
                else loot.LootValue = _enemySO.HpForPlayerLoot;
            }
        }
    }
}
