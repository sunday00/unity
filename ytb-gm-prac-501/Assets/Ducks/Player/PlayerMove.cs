using UnityEngine;

namespace Ducks.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public float speed;

        private float _axisDh;
        private float _axisDv;
        private Vector3 _axisMove;

        private Rigidbody _compRigidbody;
        private PlayerManager _playerManager;

        private bool _stateIsDodging;
        private bool _stateIsEquipping;
        private bool _stateIsJumping;

        public bool IsActing => _stateIsDodging || _stateIsJumping || _stateIsEquipping;

        public Animator GetAnimator { get; private set; }

        private void Awake()
        {
            _playerManager = gameObject.GetComponent<PlayerManager>();
            GetAnimator = GetComponentInChildren<Animator>();
            _compRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var (isRun, isJump) = GetInput();
            Move(isRun);
            Turn();
            Jump(isJump);
            Dodge(isJump);

            // foreach (var item in _playerManager.PlayerItem.Inventory) print(item.Key + ":" + item.Value);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag.Equals("Floor"))
            {
                _stateIsJumping = false;
                GetAnimator.SetBool(Constants.IsJump, false);
            }
        }

        private bool[] GetInput()
        {
            if (!_stateIsDodging && !_stateIsEquipping)
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

            GetAnimator.SetBool(Constants.IsWalk, !_axisMove.Equals(Vector3.zero));
            GetAnimator.SetBool(Constants.IsRun, isRun);
        }

        private void Turn()
        {
            transform.LookAt(transform.position + _axisMove);
        }

        private void Jump(bool isJump)
        {
            if (!_axisMove.Equals(Vector3.zero)) return;

            if (IsActing) return;

            if (isJump)
            {
                _stateIsJumping = true;
                _compRigidbody.AddForce(Vector3.up * 20, ForceMode.Impulse);

                GetAnimator.SetBool(Constants.IsJump, true);
                GetAnimator.SetTrigger(Constants.DoJump);
            }
        }

        private void Dodge(bool isJump)
        {
            if (_axisMove.Equals(Vector3.zero)) return;

            if (IsActing) return;

            if (isJump)
            {
                _stateIsDodging = true;
                speed *= 2;
                GetAnimator.SetBool(Constants.DoDodge, true);

                Invoke("DodgeFin", 0.5f);
            }
        }

        private void DodgeFin()
        {
            speed /= 2;
            _stateIsDodging = false;
        }

        public void BlinkEquipState()
        {
            _stateIsEquipping = !_stateIsEquipping;
            Invoke("BlinkEquipState", 0.4f);
        }
    }
}