using UnityEngine;
using UnityEngine.UI;

namespace Ducks.Object
{
    public class Item : MonoBehaviour
    {
        public ItemData data;
        public int level;
        public Weapon weapon;

        private Image icon;
        private Text textLevel;

        private void Awake()
        {
            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.itemIcon;

            var texts = GetComponentsInChildren<Text>();
            textLevel = texts[0];
        }

        private void LateUpdate()
        {
            textLevel.text = "Lv." + level;
        }

        public void OnClick()
        {
            switch (data.itemType)
            {
                case ItemData.ItemType.Melee:
                case ItemData.ItemType.Range:
                    if (level == 1)
                    {
                        var newWeapon = new GameObject();
                        weapon = newWeapon.AddComponent<Weapon>();
                        weapon.Init(data);
                    }
                    else
                    {
                        var nextDamage = data.baseDamage;
                        var nextCount = 0;

                        nextDamage += data.baseDamage * data.damages[level];
                        nextCount += data.counts[level];

                        weapon.LevelUp(nextDamage, nextCount);
                    }

                    break;
                case ItemData.ItemType.Glove:
                    break;
                case ItemData.ItemType.Shoe:
                    break;
                case ItemData.ItemType.Heal:
                    break;
            }

            level++;

            if (level == data.damages.Length) GetComponent<Button>().interactable = false;
        }
    }
}