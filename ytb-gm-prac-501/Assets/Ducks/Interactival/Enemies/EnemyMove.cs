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

        private void Awake()
        {
            if (isTest) return;
            _manager = GetComponent<EnemyManager>();
            _nav = GetComponent<NavMeshAgent>();

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
            var targetRadius = 1.5f;
            var targetRange = 3f;

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

            yield return new WaitForSeconds(0.2f);
            meleeArea.enabled = true;

            yield return new WaitForSeconds(1f);
            meleeArea.enabled = false;

            _manager.animator.SetBool("IsAttack", false);

            _isChase = true;
            isAttacking = false;
        }
    }
}