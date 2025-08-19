using UnityEngine;
using UnityEngine.UI;

namespace Ducks.Object
{
    public class Item : MonoBehaviour
    {
        public ItemData data;
        public int level;
        public Weapon weapon;
        public Gear gear;

        private Image icon;
        private Text textDesc;
        private Text textLevel;

        private Text textName;

        private void Awake()
        {
            icon = GetComponentsInChildren<Image>()[1];
            icon.sprite = data.itemIcon;

            var texts = GetComponentsInChildren<Text>();
            textLevel = texts[0];
            textName = texts[1];
            textDesc = texts[2];

            textName.text = data.itemName;
        }

        private void OnEnable()
        {
            textLevel.text = "Lv." + level;

            switch (data.itemType)
            {
                case ItemData.ItemType.Melee:
                case ItemData.ItemType.Range:
                    textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100, data.counts[level]);
                    break;
                case ItemData.ItemType.Glove:
                case ItemData.ItemType.Shoe:
                    textDesc.text = string.Format(data.itemDesc, data.damages[level] * 100);
                    break;
                default:
                    textDesc.text = data.itemDesc;
                    break;
            }
        }

        // private void LateUpdate()
        // {
        //     textLevel.text = "Lv." + level;
        // }

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
                case ItemData.ItemType.Shoe:
                    if (level == 1)
                    {
                        var newGear = new GameObject();
                        gear = newGear.AddComponent<Gear>();
                        gear.Init(data);
                    }
                    else
                    {
                        var nextRate = data.damages[level];
                        gear.LevelUp(nextRate);
                    }

                    break;
                case ItemData.ItemType.Heal:
                    GameManager.Instance.health = GameManager.Instance.maxHealth;
                    break;
            }

            if (data.itemType != ItemData.ItemType.Heal) level++;

            if (level == data.damages.Length) GetComponent<Button>().interactable = false;
        }
    }
}