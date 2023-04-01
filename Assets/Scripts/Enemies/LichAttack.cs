using UnityEngine;

namespace Enemies
{
    public class LichAttack : EnemyAttack
    {
        [SerializeField] private ParticleSystem _lichAttackEffect;

        public override void MakeDamage()
        {
            base.MakeDamage();

            _lichAttackEffect?.Play();
        }
    }
}

