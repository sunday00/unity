using System.Collections;
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
        }
    }
}