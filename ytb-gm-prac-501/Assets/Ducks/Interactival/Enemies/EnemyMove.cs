using UnityEngine;
using UnityEngine.AI;

namespace Ducks.Interactival.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        public Transform target;
        public bool isTest;

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

            _nav.SetDestination(target.position);
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
    }
}