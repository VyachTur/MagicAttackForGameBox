using Effects;

namespace Player.AbilityInfo
{
    public static class AreaAttackInfo
    {
        public static int MaxLevel = 5;
        public static EffectType CurrentAttackType = EffectType.ArcaneAreaEffect;
        public static int LevelNumber = 0;
        public static float Damage = 0f;
        public static float AttackRadius = 3f;
        public static float ReloadTime = 16f;

        public static void Reset()
        {
            CurrentAttackType = EffectType.ArcaneAreaEffect;
            LevelNumber = 0;
            Damage = 0f;
            AttackRadius = 3f;
            ReloadTime = 16f;
        }
    }
}
