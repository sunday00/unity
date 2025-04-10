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

        private MeshRenderer _mr;
        private Color _originalColor;
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _mr = GetComponentInChildren<MeshRenderer>();
            _originalColor = _mr.material.color;
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

        public void HitByGrenade(Vector3 pos)
        {
            curHealth -= 80;
            StartCoroutine(OnDamageTaken(
                (transform.position - pos).normalized * 25,
                true)
            );
        }

        private IEnumerator OnDamageTaken(Vector3 nuckBack, bool isGrenade = false)
        {
            _mr.material.color = Color.red;

            yield return new WaitForSeconds(0.1f);

            if (curHealth > 0)
            {
                _mr.material.color = _originalColor;
                _rb.AddForce((nuckBack + Vector3.up) * 5f, ForceMode.Impulse);
                // _rb.AddForce(nuckBack * 0.0000001f, ForceMode.Impulse);

                if (isGrenade)
                {
                    _rb.freezeRotation = false;
                    _rb.AddTorque((nuckBack + Vector3.up) * 25f, ForceMode.Impulse);
                }
            }
            else
            {
                _mr.material.color = Color.gray;
                gameObject.layer = 15;

                Destroy(gameObject, 4f);
            }
        }
    }
}