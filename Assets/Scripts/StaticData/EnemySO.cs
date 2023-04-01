using UnityEngine;
using Enemies;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Create", menuName = "Enemy", order = 51)]
    public class EnemySO : ScriptableObject
    {
        public EnemyType EnemyType;
        public int MaxHealth;
        public float Damage;
        public float AttackDistance;
        public float OffsetY;
        public int ScoreForPlayer;
        public float ExpForPlayerLoot;
        public float HpForPlayerLoot;
        public float EnemyCastSphereForAttack;
    }
}

