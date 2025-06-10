using UnityEngine;

namespace Ducks.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public Rigidbody2D target;

        public bool isLive;

        [Header("---components---")] //
        public SpriteRenderer sprite;

        public Rigidbody2D rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            var dir = target.position - rigid.position;
            var nextVec = dir.normalized * speed * Time.fixedDeltaTime;

            rigid.MovePosition(rigid.position + nextVec);

            rigid.linearVelocity = Vector2.zero;
        }
    }
}