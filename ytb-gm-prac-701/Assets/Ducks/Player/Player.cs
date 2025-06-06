using UnityEngine;
using UnityEngine.InputSystem;

namespace Ducks.Player
{
    public class Player : MonoBehaviour
    {
        public float speed = 3f;
        private Vector2 _inputVec;
        private Rigidbody2D _rigid;

        private void Awake()
        {
            _rigid = GetComponent<Rigidbody2D>();
            speed = 3f;
        }

        private void Update()
        {
            // _inputVec.x = Input.GetAxisRaw("Horizontal");
            // _inputVec.y = Input.GetAxisRaw("Vertical");
        }

        private void FixedUpdate()
        {
            // _rigid.AddForce(_inputVec);
            // _rigid.linearVelocity = _inputVec;
            // var nextVec = _inputVec.normalized * speed * Time.fixedDeltaTime;
            var nextVec = _inputVec * speed * Time.fixedDeltaTime;

            _rigid.MovePosition(_rigid.position + nextVec);
        }

        private void OnMove(InputValue value)
        {
            _inputVec = value.Get<Vector2>();
        }
    }
}