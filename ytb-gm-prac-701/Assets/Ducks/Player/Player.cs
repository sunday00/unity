using UnityEngine;
using UnityEngine.InputSystem;

namespace Ducks.Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 3f;
        private Animator _animator;
        private Vector2 _inputVec;
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
        }

        private void FixedUpdate()
        {
            var nextVec = _inputVec * speed * Time.fixedDeltaTime;

            _rigid.MovePosition(_rigid.position + nextVec);
        }

        private void LateUpdate()
        {
            _animator.SetFloat("Speed", _inputVec.magnitude);
            if (!_inputVec.x.Equals(0)) _spriter.flipX = _inputVec.x < 0;
        }

        private void OnMove(InputValue value)
        {
            _inputVec = value.Get<Vector2>();
        }
    }
}