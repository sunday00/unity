using UnityEngine;

namespace Ducks
{
    public class MapReposition : MonoBehaviour
    {
        public Collider2D col;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Area")) return;

            var playerPos = GameManager.Instance.player.transform.position;
            var myPos = transform.position;

            var diffX = Mathf.Abs(playerPos.x - myPos.x);
            var diffY = Mathf.Abs(playerPos.y - myPos.y);

            var playerDir = GameManager.Instance.player.inputVec;
            var dirX = playerDir.x < 0 ? -1 : 1;
            var dirY = playerDir.y < 0 ? -1 : 1;

            switch (transform.tag)
            {
                // TODO: How to rectangle?? this only works on SQUARE shape.
                case "Ground":
                    if (diffX > diffY)
                        transform.Translate(Vector2.right * dirX * 40);
                    else if (diffX < diffY)
                        transform.Translate(Vector2.up * dirY * 40);
                    break;
                case "Enemy":
                    if (col.enabled)
                        transform.Translate(
                            playerDir * 20
                            + new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f))
                        );
                    break;
            }
        }
    }
}