using Ducks.Player;
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
            var spd = 0f;

            foreach (var weapon in weapons)
                switch (weapon.id)
                {
                    case 0:
                        spd = 150 * Character.WeaponSpeed;
                        weapon.speed = spd + spd * rate;
                        break;
                    default:
                        spd = 0.5f * Character.WeaponRate;
                        weapon.speed = spd * (1f - rate);
                        break;
                }
        }

        public void SpeedUp()
        {
            var speed = 3 * Character.Speed;
            GameManager.Instance.player.speed = speed + speed * rate;
        }
    }
}