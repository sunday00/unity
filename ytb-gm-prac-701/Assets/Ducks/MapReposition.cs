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


            switch (transform.tag)
            {
                // TODO: How to rectangle?? this only works on SQUARE shape.
                case "Ground":
                    var diffX = playerPos.x - myPos.x;
                    var diffY = playerPos.y - myPos.y;

                    var dirX = diffX < 0 ? -1 : 1;
                    var dirY = diffY < 0 ? -1 : 1;

                    diffX = Mathf.Abs(diffX);
                    diffY = Mathf.Abs(diffY);

                    if (diffX > diffY)
                        transform.Translate(Vector2.right * dirX * 40);
                    else if (diffX < diffY)
                        transform.Translate(Vector2.up * dirY * 40);
                    break;
                case "Enemy":
                    if (col.enabled)
                    {
                        var dist = playerPos - myPos;
                        var ran = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
                        transform.Translate(ran + dist * 2);
                    }

                    break;
            }
        }
    }
}