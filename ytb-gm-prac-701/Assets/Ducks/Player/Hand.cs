using UnityEngine;

namespace Ducks.Player
{
    public class Hand : MonoBehaviour
    {
        public bool isLeft;
        public SpriteRenderer sprite;
        private readonly Quaternion leftRot = Quaternion.Euler(0, 0, -35);
        private readonly Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

        private readonly Vector3 rightPos = new(0.35f, -0.15f, 0);
        private readonly Vector3 rightPosReverse = new(-0.15f, -0.15f, 0);
        private SpriteRenderer player;

        private void Awake()
        {
            player = GetComponentsInParent<SpriteRenderer>()[1];
        }

        private void LateUpdate()
        {
            var isReverse = player.flipX;

            if (isLeft)
            {
                transform.localRotation = isReverse ? leftRotReverse : leftRot;
                sprite.flipY = isReverse;
                sprite.sortingOrder = isReverse ? 6 : 4;
            }
            else
            {
                transform.localPosition = isReverse ? rightPosReverse : rightPos;
                sprite.flipX = isReverse;
                sprite.sortingOrder = isReverse ? 4 : 6;
            }
        }
    }
}