using System.Collections;
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

        private WaitForFixedUpdate wait;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();
            isLive = true;

            wait = new WaitForFixedUpdate();
        }

        private void FixedUpdate()
        {
            if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

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

            StartCoroutine(KnockBack());

            if (health > 0)
                Damaged();
            else
                Dead();
        }

        private void Damaged()
        {
            anim.SetTrigger("Hit");
        }

        private IEnumerator KnockBack()
        {
            // yield return null;
            //
            // yield return new WaitForSeconds(0.2f);

            yield return wait;

            var playerPos = GameManager.Instance.player.transform.position;
            var dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized * 3f, ForceMode2D.Impulse);
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