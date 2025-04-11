using System.Collections;
using Ducks.Interactival.Enemies;
using UnityEngine;

namespace Ducks.Interactival.Items
{
    public class GrenadeBoom : MonoBehaviour
    {
        public GameObject meshObj;
        public GameObject effectObj;
        public Rigidbody rb;

        private void Start()
        {
            StartCoroutine(Explode());
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(2f);

            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            meshObj.SetActive(false);
            effectObj.SetActive(true);

            var rayHits = Physics.SphereCastAll(
                transform.position,
                15,
                Vector3.up,
                0f,
                LayerMask.GetMask("Enemy")
            );

            foreach (var hit in rayHits) hit.transform.GetComponent<EnemyDamage>().HitByGrenade(transform.position);

            Destroy(gameObject, 5);
        }
    }
}