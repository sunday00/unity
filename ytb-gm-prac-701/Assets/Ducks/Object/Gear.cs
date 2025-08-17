using UnityEngine;

namespace Ducks.Object
{
    public class Gear : MonoBehaviour
    {
        public ItemData.ItemType type;
        public float rate;

        public void Init(ItemData data)
        {
            name = "Gear " + data.itemId;
            transform.parent = GameManager.Instance.player.transform;
            transform.localPosition = Vector3.zero;

            type = data.itemType;
            rate = data.damages[0];

            ApplyGear();
        }

        public void LevelUp(float localRate)
        {
            rate = localRate;
            ApplyGear();
        }

        public void ApplyGear()
        {
            switch (type)
            {
                case ItemData.ItemType.Glove: RateUp(); break;
                case ItemData.ItemType.Shoe: SpeedUp(); break;
            }
        }

        public void RateUp()
        {
            var weapons = transform.parent.GetComponentsInChildren<Weapon>();

            foreach (var weapon in weapons)
                switch (weapon.id)
                {
                    case 0: weapon.speed = 150 + 150 * rate; break;
                    default: weapon.speed = 0.5f * (1f - rate); break;
                }
        }

        public void SpeedUp()
        {
            float speed = 3;
            GameManager.Instance.player.speed = speed + speed * rate;
        }
    }
}