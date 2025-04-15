using Ducks.Interactival.Items;
using UnityEngine;
using UnityEngine.AI;

namespace Ducks.Interactival.Enemies
{
    public class BossBullet : BulletAction
    {
        public Transform target;
        private NavMeshAgent _nav;

        private void Awake()
        {
            _nav = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            _nav.SetDestination(target.position);
        }
    }
}