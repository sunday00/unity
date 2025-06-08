using UnityEngine;
using UnityEngine.InputSystem;

namespace Ducks.Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 3f;
        public Vector2 inputVec;

        private Animator _animator;
        private Rigidbody2D _rigid;
        private SpriteRenderer _spriter;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            _spriter = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            speed = 3f;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire3")) speed = 5f;
            if (Input.GetButtonUp("Fire3")) speed = 3f;
        }

        private void FixedUpdate()
        {
            var nextVec = inputVec * speed * Time.fixedDeltaTime;

            _rigid.MovePosition(_rigid.position + nextVec);
        }

        private void LateUpdate()
        {
            _animator.SetFloat("Speed", inputVec.magnitude);
            if (!inputVec.x.Equals(0)) _spriter.flipX = inputVec.x < 0;
        }

        private void OnMove(InputValue value)
        {
            inputVec = value.Get<Vector2>();
        }
    }
}