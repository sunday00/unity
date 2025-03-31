namespace Ducks
{
    public static class Constants
    {
        public enum Type
        {
            Ammo,
            Coin,
            Grenade,
            Heart,
            Weapon
        }

        public static readonly string IsWalk = "IsWalk";
        public static readonly string IsRun = "IsRun";
        public static readonly string IsJump = "IsJump";
        public static readonly string DoJump = "DoJump";
        public static readonly string DoDodge = "DoDodge";
    }
}