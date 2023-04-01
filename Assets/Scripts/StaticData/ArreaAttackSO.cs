using UnityEngine;
using Effects;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Create", menuName = "AreaAttack", order = 53)]
    public class ArreaAttackSO : ScriptableObject
    {
        public EffectType EffectType;
        public float Damage = 1f;
        public float DamageActivateAfterSeconds = 1.15f;
        public float DamageMakeRadius = 3f;
        public float Power = 3f;
        public float UpForce = 3f;
    }
}

