using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Ducks.Interactival.Enemies
{
    public class BossMove : MonoBehaviour, IEnemyMove
    {
        public Transform target;
        public BoxCollider meleeArea;
        public GameObject missile;

        public bool isAttacking;

        private bool _isChase;

        private EnemyManager _manager;
        private NavMeshAgent _nav;
        private Rigidbody _rb;

        private void Awake()
        {
            _manager = GetComponent<EnemyManager>();
            _nav = GetComponent<NavMeshAgent>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_isChase) return;

            if (!_nav.enabled) return;

            // if (!_nav.isStopped) return;

            // _nav.SetDestination(target.position);
        }

        private void FixedUpdate()
        {
        }

        public void SetChase(bool isChase)
        {
            _isChase = isChase;
        }

        public void SetNav(bool active)
        {
            _nav.enabled = active;
        }

        public bool GetIsTest()
        {
            return false;
        }

        private IEnumerator Attacking()
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
}