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

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            _axisDh = Input.GetAxisRaw("Horizontal");
            _axisDv = Input.GetAxisRaw("Vertical");
            var isRun = Input.GetButton("Run");

            _axisMove = new Vector3(_axisDh, 0, _axisDv).normalized;

            transform.position += _axisMove * speed * (isRun ? 1 : 0.5f) * Time.deltaTime;

            _animator.SetBool("IsWalk", !_axisMove.Equals(Vector3.zero));
            _animator.SetBool("IsRun", isRun);

            transform.LookAt(transform.position + _axisMove);
        }
    }
}