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

    public static class Boss
    {
        public static readonly string Idle = "Idle";
        public static readonly string Run = "Run";
        public static readonly string Shoot = "Shoot";
        public static readonly string Melee = "Melee";
        public static readonly string Missiles = "Missiles";
        public static readonly string Death = "Death";
    }

    public static class Enemy
    {
        public static readonly string Idle = "Idle";
        public static readonly string Run = "Run";
        public static readonly string Melee = "Melee";
        public static readonly string Shoot = "Shoot";
        public static readonly string Death = "Death";
    }

    public static class Turret
    {
        public static readonly string Idle = "Idle";
        public static readonly string Shoot = "Shoot";
        public static readonly string Death = "Death";
    }

    public static class Door
    {
        public static readonly string Lock = "Lock";
        public static readonly string Unlock = "Unlock";
        public static readonly string Open = "Open";
        public static readonly string Close = "Close";
    }
}
