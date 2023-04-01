namespace Player.AbilityInfo
{
    public static class MagicProjectileInfo
    {
        public static int MaxLevel = 15;
        public static int LevelNumber = 0;
        public static float Damage = 0f;

        public static void Reset()
        {
            LevelNumber = 0;
            Damage = 0f;
        }
    }
}

