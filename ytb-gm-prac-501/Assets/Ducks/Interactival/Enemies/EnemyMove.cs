using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Ducks.Interactival.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        public Transform target;
        public bool isTest;

        public BoxCollider meleeArea;
        public bool isAttacking;

        private bool _isChase;

        private EnemyManager _manager;
        private NavMeshAgent _nav;
        private Rigidbody _rb;

        private void Awake()
        {
            if (isTest) return;
            _manager = GetComponent<EnemyManager>();
            _nav = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();

            Invoke("Chase", 2f);
        }

        private void Update()
        {
            if (isTest) return;

            if (!_isChase) return;

            if (!_nav.enabled) return;

            // if (!_nav.isStopped) return;

            _nav.SetDestination(target.position);
        }

        private void FixedUpdate()
        {
            Targeting();
        }

        private void Targeting()
        {
            if (isTest) return;

            var targetRadius = 0f;
            var targetRange = 0f;

            switch (_manager.Type)
            {
                case Constants.EnemyType.A:
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;
                case Constants.EnemyType.B:
                    targetRadius = 1f;
                    targetRange = 15f;
                    break;
                case Constants.EnemyType.C:
                    targetRadius = 1.5f;
                    targetRange = 3f;
                    break;
            }

            var hit = Physics.SphereCastAll(
                transform.position,
                targetRadius,
                transform.forward,
                targetRange,
                LayerMask.GetMask("Player")
            );

            if (hit.Length > 0 && !isAttacking) StartCoroutine(Attacking());
        }

        private void Chase()
        {
            _isChase = true;
            _manager.animator.SetBool(Constants.IsWalk, true);
        }

        public void SetChase(bool isChase)
        {
            _isChase = isChase;
        }

        public void SetNav(bool active)
        {
            _nav.enabled = active;
        }

        private IEnumerator Attacking()
        {
            _isChase = false;
            isAttacking = true;
            _manager.animator.SetBool("IsAttack", true);

            switch (_manager.Type)
            {
                case Constants.EnemyType.A:
                    yield return new WaitForSeconds(0.2f);
                    meleeArea.enabled = true;

                    yield return new WaitForSeconds(1f);
                    meleeArea.enabled = false;
                    break;
                case Constants.EnemyType.B:
                    yield return new WaitForSeconds(0.1f);
                    meleeArea.enabled = true;
                    _rb.AddForce(transform.forward * 30f, ForceMode.Impulse);

                    yield return new WaitForSeconds(1f);
                    _rb.linearVelocity = Vector3.zero;
                    meleeArea.enabled = false;

                    yield return new WaitForSeconds(2f);

                    break;
                case Constants.EnemyType.C:
                    break;
            }

            _manager.animator.SetBool("IsAttack", false);

            _isChase = true;
            isAttacking = false;
        }
    }
}