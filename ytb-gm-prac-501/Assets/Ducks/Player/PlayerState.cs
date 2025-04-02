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
                    case Constants.Type.Ammo:
                        curAmmo = curAmmo + item.value >= maxAmmo ? maxAmmo : curAmmo + item.value;
                        break;
                    case Constants.Type.Coin:
                        curCoin = curCoin + item.value >= maxCoin ? maxCoin : curCoin + item.value;
                        break;
                    case Constants.Type.Grenade:
                        curGrenades = curGrenades + item.value >= maxGrenades ? maxGrenades : curGrenades + item.value;
                        break;
                    case Constants.Type.Heart:
                        curHealth = curHealth + item.value >= maxHealth ? maxHealth : curHealth + item.value;
                        break;
                }

                Destroy(other.gameObject);
            }
        }
    }
}