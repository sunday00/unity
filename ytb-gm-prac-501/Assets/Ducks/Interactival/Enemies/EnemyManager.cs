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

            var nuckBack = (transform.position - other.transform.position).normalized;

            StartCoroutine(OnDamageTaken(nuckBack));
            curHealth -= damage;
        }

        private IEnumerator OnDamageTaken(Vector3 nuckBack)
        {
            var mat = GetComponent<MeshRenderer>().material;
            var originalColor = mat.color;

            mat.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            if (curHealth > 0)
            {
                mat.color = originalColor;
                _rb.AddForce((nuckBack + Vector3.up) * 5f, ForceMode.Impulse);
                // _rb.AddForce(nuckBack * 0.0000001f, ForceMode.Impulse);
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