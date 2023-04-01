using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Create", menuName = "MagicProjectile", order = 52)]
    public class MagicProjectileSO : ScriptableObject
    {
        public float FlySpeed = 34f;
        public float Damage = 1f;
    }
}
