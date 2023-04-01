using UnityEngine;

namespace Enemies
{
    public class EnemyLootVariants : MonoBehaviour
    {
        [SerializeField] private GameObject[] _lootObjects;

        public virtual GameObject GetRandomLoot() => 
            _lootObjects[Random.Range(0, _lootObjects.Length)];
    }
}

