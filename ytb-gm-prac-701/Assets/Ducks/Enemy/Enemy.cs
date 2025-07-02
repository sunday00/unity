using Ducks.Object;
using UnityEngine;

namespace Ducks.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public float speed;
        public float health;
        public float maxHealth;
        public RuntimeAnimatorController[] animCon;
        public Rigidbody2D target;

        public bool isLive;

        [Header("---components---")] //
        public SpriteRenderer sprite;

        public Rigidbody2D rigid;

        public Animator anim;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            isLive = true;
        }

        private void FixedUpdate()
        {
            if (!isLive) return;

            var dir = target.position - rigid.position;
            var nextVec = dir.normalized * speed * Time.fixedDeltaTime;

            rigid.MovePosition(rigid.position + nextVec);

            rigid.linearVelocity = Vector2.zero;
        }

        private void LateUpdate()
        {
            if (!isLive) return;

            sprite.flipX = target.position.x < rigid.position.x;
            sprite.sortingOrder = target.position.y > rigid.position.y ? 6 : 2;
        }

        private void OnEnable()
        {
            target = GameManager.Instance.player.GetComponent<Rigidbody2D>();
            isLive = true;
            health = maxHealth;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Bullet")) return;

            var damage = other.GetComponent<Bullet>().damage;
            health -= damage;

            if (health > 0)
                Damaged();
            else
                Dead();
        }

        private void Damaged()
        {
        }

        private void Dead()
        {
            gameObject.SetActive(false);
        }

        public void Init(SpawnData spawnData)
        {
            anim.runtimeAnimatorController = animCon[spawnData.spriteType];
            speed = spawnData.speed;
            maxHealth = spawnData.health;
            health = spawnData.health;
        }
    }
}