using UnityEngine;
using UnityEngine.InputSystem;

namespace Ducks.Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 3f;
        public Vector2 inputVec;
        public Scanner scanner;

        public Hand[] hands;
        public RuntimeAnimatorController[] animCon;

        private Animator _animator;
        private Rigidbody2D _rigid;
        private SpriteRenderer _spriter;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _spriter = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            scanner = GetComponent<Scanner>();

            speed = 3f;

            hands = GetComponentsInChildren<Hand>(true);
        }

        private void Update()
        {
            if (!GameManager.Instance.isLive) return;

            if (Input.GetButtonDown("Fire3")) speed = 5f;
            if (Input.GetButtonUp("Fire3")) speed = 3f;
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.isLive) return;

            var nextVec = inputVec * speed * Time.fixedDeltaTime;

            _rigid.MovePosition(_rigid.position + nextVec);
        }

        private void LateUpdate()
        {
            if (!GameManager.Instance.isLive) return;

            _animator.SetFloat("Speed", inputVec.magnitude);
            if (!inputVec.x.Equals(0)) _spriter.flipX = inputVec.x < 0;
        }

        private void OnEnable()
        {
            speed *= Character.Speed;
            _animator.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!GameManager.Instance.isLive) return;

            GameManager.Instance.health -= Time.deltaTime * 10f;

            if (GameManager.Instance.health <= 0)
            {
                for (var index = 2; index < transform.childCount; index++)
                    transform.GetChild(index).gameObject.SetActive(false);

                _animator.SetTrigger("Dead");
                GameManager.Instance.GameOver();
            }
        }

        private void OnMove(InputValue value)
        {
            inputVec = value.Get<Vector2>();
        }
    }
}