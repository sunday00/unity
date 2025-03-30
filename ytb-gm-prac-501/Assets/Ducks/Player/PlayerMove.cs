using UnityEngine;

namespace Ducks.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed;

        private Animator _animator;
        private float _axisDh;
        private float _axisDv;
        private Vector3 _axisMove;

        private Rigidbody _compRigidbody;

        private bool _stateIsJumping;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _compRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var (isRun, isJump) = GetInput();
            Move(isRun);
            Turn();
            Jump(isJump);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Floor"))
            {
                _stateIsJumping = false;
                _animator.SetBool("IsJump", false);
            }
        }

        private bool[] GetInput()
        {
            _axisDh = Input.GetAxisRaw("Horizontal");
            _axisDv = Input.GetAxisRaw("Vertical");

            return new[]
            {
                Input.GetButton("Run"),
                Input.GetButtonDown("Jump")
            };
        }

        private void Move(bool isRun)
        {
            _axisMove = new Vector3(_axisDh, 0, _axisDv).normalized;

            transform.position += _axisMove * speed * (isRun ? 1 : 0.5f) * Time.deltaTime;

            _animator.SetBool(Constants.isWalk, !_axisMove.Equals(Vector3.zero));
            _animator.SetBool(Constants.isRun, isRun);
        }

        private void Turn()
        {
            transform.LookAt(transform.position + _axisMove);
        }

        private void Jump(bool isJump)
        {
            if (isJump && !_stateIsJumping)
            {
                _stateIsJumping = true;
                _compRigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);

                _animator.SetBool("IsJump", true);
                _animator.SetTrigger("DoJump");
            }
        }
    }
}