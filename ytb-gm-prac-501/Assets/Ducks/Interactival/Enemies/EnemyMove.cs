using UnityEngine;
using UnityEngine.AI;

namespace Ducks.Interactival.Enemies
{
    public class EnemyMove : MonoBehaviour
    {
        public Transform target;
        public bool isTest;

        private NavMeshAgent _nav;

        private void Awake()
        {
            if (isTest) return;
            _nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (isTest) return;
            _nav.SetDestination(target.position);
        }
    }
}