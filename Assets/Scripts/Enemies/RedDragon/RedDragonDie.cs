using UnityEngine;
using StaticData;
using System;
using Environments;

namespace Enemies.RedDragon
{
    [RequireComponent(typeof(RedDragonMove))]
    [RequireComponent(typeof(EnemyLookAtPlayer))]
    public class RedDragonDie : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _enemyRendererObject;
        [SerializeField] private EnemyLootVariants _loot;
        [SerializeField] private EnemySO _enemySO;
        [SerializeField] private Collider _dragonCollider;
 
        public static event Action<int> OnDragonMakeScoreEvent;
        public static event Action OnDragonDieEvent;

        public bool IsEnemyDeath { get; private set; }

        private EnemyMove _enemyMove;

        private void OnEnable()
        {
            IsEnemyDeath = false;
            _enemyMove = GetComponent<EnemyMove>();
            _dragonCollider.enabled = true;
            GetComponent<EnemyLookAtPlayer>().enabled = true;
        }

        public void Die()
        {
            IsEnemyDeath = true;
            _animator.SetTrigger(Constants.Die);
            _dragonCollider.enabled = false;
            GetComponent<EnemyLookAtPlayer>().enabled = false;
            OnDragonMakeScoreEvent?.Invoke(_enemySO.ScoreForPlayer);
            OnDragonDieEvent?.Invoke();

            Invoke("DeactivateObject", 3f);
        }

        private void DeactivateObject()
        {
            gameObject.SetActive(false);

            GameObject randomLoot = _loot.GetRandomLoot();
            if (randomLoot != null)
            {
                Loot loot = Instantiate(randomLoot, transform.position, Quaternion.identity)?.GetComponent<Loot>();
                
                if (loot is PlusExp) loot.LootValue = _enemySO.ExpForPlayerLoot;
                else loot.LootValue = _enemySO.HpForPlayerLoot;
            }
        }
    }
}

