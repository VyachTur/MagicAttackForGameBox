using UnityEngine;

namespace Enemies.RedDragon
{
    public class RedDragonLoot : EnemyLootVariants
    {
        [SerializeField] private GameObject _lootObject;

        public override GameObject GetRandomLoot() => 
            _lootObject;
    }
}

