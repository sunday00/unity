using Ducks.Interactival.Items;
using Unity.VisualScripting;
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
            if (target.IsUnityNull()) return;

            _nav.SetDestination(target.position);
        }
    }
}