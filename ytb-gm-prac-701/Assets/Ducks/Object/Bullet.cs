using UnityEngine;

namespace Ducks.Object
{
    public class Bullet : MonoBehaviour
    {
        public float damage;
        public int per;

        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Enemy") || per.Equals(-100)) return;

            per--;

            if (per < 0)
            {
                _rb.linearVelocity = Vector2.zero;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Area") || per.Equals(-100)) return;

            gameObject.SetActive(false);
        }

        public void Init(float damageProp, int perProp, Vector3 direction)
        {
            damage = damageProp;
            per = perProp;

            if (per >= 0) _rb.linearVelocity = direction * 15f;
        }
    }
}