using System.Collections;
using System.Linq;
using Ducks.Interactival.Items;
using Ducks.Player;
using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class BossDamage : MonoBehaviour, IEnemyDamage
    {
        public int maxHealth;
        public int curHealth;

        private BoxCollider _boxCollider;

        private EnemyManager _manager;

        private MeshRenderer[] _mrs;
        private Color[] _originalColors = { };
        private Rigidbody _rb;

        private void Awake()
        {
            _manager = GetComponent<EnemyManager>();
            _rb = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<BoxCollider>();
            _mrs = GetComponentsInChildren<MeshRenderer>();
            foreach (var mr in _mrs) _originalColors = _originalColors.Append(mr.material.color).ToArray();
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

            var nuckBack = Vector3.zero;

            StartCoroutine(OnDamageTaken(nuckBack));
            curHealth -= damage;
        }

        public void HitByGrenade(Vector3 pos)
        {
            curHealth -= 60;
            StartCoroutine(OnDamageTaken(
                (transform.position - pos).normalized * 5,
                true)
            );
        }

        private IEnumerator OnDamageTaken(Vector3 nuckBack, bool isGrenade = false)
        {
            foreach (var mr in _mrs) mr.material.color = Color.green;

            yield return new WaitForSeconds(0.1f);

            if (curHealth > 0)
            {
                for (var i = 0; i < _mrs.Length; i++) _mrs[i].material.color = _originalColors[i];

                _rb.AddForce((nuckBack + Vector3.up) * 5f, ForceMode.Impulse);
                // _rb.AddForce(nuckBack * 0.0000001f, ForceMode.Impulse);

                if (isGrenade)
                {
                    _rb.freezeRotation = false;
                    _rb.AddTorque((nuckBack + Vector3.up) * 5f, ForceMode.Impulse);
                }
            }
            else
            {
                foreach (var mr in _mrs) mr.material.color = Color.gray;

                gameObject.layer = 15;

                _manager.animator.SetTrigger("DoDie");
                _manager.EnemyMove.SetChase(false);
                _manager.EnemyMove.SetNav(false);

                Destroy(gameObject, 4f);
            }
        }
    }
}