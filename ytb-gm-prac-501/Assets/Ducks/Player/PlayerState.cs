using System.Collections;
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
        private MeshRenderer[] _meshes;

        private bool _onDamage;

        private void Awake()
        {
            _meshes = GetComponentsInChildren<MeshRenderer>();
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

            if (other.tag.Equals("EnemyBullet"))
            {
                var bullet = other.GetComponent<BulletAction>();
                curHealth -= bullet.damage;

                if (other.name.Contains("Missile") || other.name.Contains("BossRock")) Destroy(other.gameObject);

                if (_onDamage) return;

                StartCoroutine(OnDamageTaken());
            }
        }

        private void GettingGrenade(ItemReact item)
        {
            curGrenades = curGrenades + item.value >= maxGrenades ? maxGrenades : curGrenades + item.value;
            equippedGrenades[curGrenades - 1].gameObject.SetActive(true);
        }

        private IEnumerator OnDamageTaken()
        {
            _onDamage = true;
            foreach (var mesh in _meshes) mesh.material.color = Color.yellow;

            yield return new WaitForSeconds(1f);

            foreach (var mesh in _meshes) mesh.material.color = Color.white;
            _onDamage = false;
        }
    }
}