using Ducks.Interactival.Items;
using UnityEngine;

namespace Ducks.Player
{
    public class PlayerState : MonoBehaviour
    {
        public int curAmmo;
        public int curHealth;
        public int curCoin;
        public int curGrenades;

        public int maxAmmo;
        public int maxHealth;
        public int maxCoin;
        public int maxGrenades;

        public GameObject[] equippedGrenades;

        private PlayerManager _playerManager;

        private void Awake()
        {
            _playerManager = gameObject.GetComponent<PlayerManager>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Item"))
            {
                var item = other.GetComponent<ItemReact>();
                switch (item.type)
                {
                    case Constants.ItemType.Ammo:
                        curAmmo = curAmmo + item.value >= maxAmmo ? maxAmmo : curAmmo + item.value;
                        break;
                    case Constants.ItemType.Coin:
                        curCoin = curCoin + item.value >= maxCoin ? maxCoin : curCoin + item.value;
                        break;
                    case Constants.ItemType.Grenade:
                        GettingGrenade(item);
                        break;
                    case Constants.ItemType.Heart:
                        curHealth = curHealth + item.value >= maxHealth ? maxHealth : curHealth + item.value;
                        break;
                }

                Destroy(other.gameObject);
            }
        }

        private void GettingGrenade(ItemReact item)
        {
            curGrenades = curGrenades + item.value >= maxGrenades ? maxGrenades : curGrenades + item.value;
            equippedGrenades[curGrenades - 1].gameObject.SetActive(true);
        }
    }
}