namespace Player.AbilityInfo
{
    public static class DashInfo
    {
        public static int MaxLevel = 9;
        public static int LevelNumber = 0;
        public static float DashPower = 26f;
        public static float ReloadTime = 7f;

        public static void Reset()
        {
            LevelNumber = 0;
            DashPower = 26f;
            ReloadTime = 7f;
        }
    }
}
