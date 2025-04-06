namespace Ducks
{
    public static class Constants
    {
        public enum ItemType
        {
            Ammo,
            Coin,
            Grenade,
            Heart,
            Weapon
        }

        public enum WeaponType
        {
            Melee,
            Range
        }

        public static readonly string IsWalk = "IsWalk";
        public static readonly string IsRun = "IsRun";
        public static readonly string IsJump = "IsJump";
        public static readonly string DoJump = "DoJump";
        public static readonly string DoDodge = "DoDodge";
        public static readonly string DoEquip = "DoEquip";
        public static readonly string DoSwing = "DoSwing";
        public static readonly string DoShot = "DoShot";
        public static readonly string DoReload = "DoReload";
    }
}