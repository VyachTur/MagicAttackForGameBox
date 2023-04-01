using UnityEngine;

namespace Enemies
{
    public class GolemAttack : EnemyAttack
    {
        [SerializeField] private ParticleSystem _golemAttackEffect;

        public override void MakeDamage()
        {
            base.MakeDamage();

            _golemAttackEffect?.Play();
        }
    }
}

