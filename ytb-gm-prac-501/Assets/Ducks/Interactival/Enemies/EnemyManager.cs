using System.Collections;
using Ducks.Interactival.Items;
using Ducks.Player;
using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class EnemyManager : MonoBehaviour
    {
        public int maxHealth;
        public int curHealth;

        private BoxCollider _boxCollider;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            var damage = 0;

            switch (other.tag)
            {
                case "Melee":
                    damage = other.GetComponent<PlayerWeaponMelee>().damage;
                    break;
                case "Bullet":
                    damage = other.GetComponent<BulletAction>().damage;
                    Destroy(other.gameObject);
                    break;
                default: return;
            }

            StartCoroutine(OnDamageTaken());
            curHealth -= damage;
        }

        private IEnumerator OnDamageTaken()
        {
            var mat = GetComponent<MeshRenderer>().material;
            var originalColor = mat.color;

            mat.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            if (curHealth > 0)
            {
                mat.color = originalColor;
            }
            else
            {
                mat.color = Color.gray;
                gameObject.layer = 15;
                Destroy(gameObject, 4f);
            }
        }
    }
}