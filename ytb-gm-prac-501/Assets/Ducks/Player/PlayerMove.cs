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

        private bool _stateIsDodging;
        private bool _stateIsJumping;

        public bool IsActing => _stateIsDodging || _stateIsJumping;

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
            Dodge(isJump);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Floor"))
            {
                _stateIsJumping = false;
                _animator.SetBool(Constants.IsJump, false);
            }
        }

        private bool[] GetInput()
        {
            if (!_stateIsDodging)
            {
                _axisDh = Input.GetAxisRaw("Horizontal");
                _axisDv = Input.GetAxisRaw("Vertical");
            }

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

            _animator.SetBool(Constants.IsWalk, !_axisMove.Equals(Vector3.zero));
            _animator.SetBool(Constants.IsRun, isRun);
        }

        private void Turn()
        {
            transform.LookAt(transform.position + _axisMove);
        }

        private void Jump(bool isJump)
        {
            if (!_axisMove.Equals(Vector3.zero)) return;

            if (_stateIsDodging) return;

            if (isJump && !_stateIsJumping)
            {
                _stateIsJumping = true;
                _compRigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);

                _animator.SetBool(Constants.IsJump, true);
                _animator.SetTrigger(Constants.DoJump);
            }
        }

        private void Dodge(bool isJump)
        {
            if (_axisMove.Equals(Vector3.zero)) return;

            if (_stateIsDodging) return;

            if (isJump && !_stateIsJumping)
            {
                _stateIsDodging = true;
                speed *= 2;
                _animator.SetBool(Constants.DoDodge, true);

                Invoke("DodgeFin", 0.5f);
            }
        }

        private void DodgeFin()
        {
            speed /= 2;
            _stateIsDodging = false;
        }
    }
}