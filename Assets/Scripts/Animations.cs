public static class Animations
{
    public static class Player
    {
        public static readonly string Idle = "Idle";
        public static readonly string Run = "Run";
        public static readonly string Shoot = "Shoot";

        public static class Parameters
        {
            public static readonly string ShootingSpeedMultiplier = "ShootingSpeedMultiplier";
        }
    }

    public static class Enemy
    {
        public static readonly string Idle = "Idle";
        public static readonly string Run = "Run";
        public static readonly string Shoot = "Shoot";
        public static readonly string Death = "Death";
    }
}
