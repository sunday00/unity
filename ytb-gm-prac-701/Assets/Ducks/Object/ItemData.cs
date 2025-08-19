using UnityEngine;

namespace Ducks.Object
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemData")]
    public class ItemData : ScriptableObject
    {
        public enum ItemType
        {
            Melee,
            Range,
            Glove,
            Shoe,
            Heal
        }

        [Header("---Main Information---")] //
        public ItemType itemType;

        public int itemId;

        public string itemName;

        [TextArea] //
        public string itemDesc;

        public Sprite itemIcon;

        [Header("---Level Data---")] //
        public float baseDamage;

        public int baseCount;
        public float[] damages;
        public int[] counts;

        [Header("---Weapon---")] //
        public GameObject projectile;

        public Sprite hand;
    }
}