using UnityEngine;

namespace Ducks.Player
{
    public class Scanner : MonoBehaviour
    {
        public float scanRange;
        public LayerMask targetLayer;
        public Transform nearestTarget;
        public RaycastHit2D[] targets;

        private void FixedUpdate()
        {
            targets = Physics2D.CircleCastAll(
                transform.position,
                scanRange,
                Vector2.zero,
                0,
                targetLayer
            );

            nearestTarget = GetNearestTarget();
        }

        private Transform GetNearestTarget()
        {
            Transform result = null;
            float diff = 100;

            foreach (var target in targets)
            {
                var myPos = transform.position;
                var targetPos = target.transform.position;

                var curDiff = Vector3.Distance(myPos, targetPos);

                if (curDiff < diff)
                {
                    diff = curDiff;
                    result = target.transform;
                }
            }

            return result;
        }
    }
}