using UnityEngine;
using StaticData;

namespace Enemies
{
    public class EnemyId : MonoBehaviour
    {
        [SerializeField] private EnemySO _enemySO;

        public EnemyType EnemyType => _enemySO.EnemyType;
    }
}
