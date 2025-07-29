using UnityEngine;

namespace Ducks
{
    public class Follow : MonoBehaviour
    {
        public RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            rectTransform.position = Camera.main.WorldToScreenPoint(GameManager.Instance.player.transform.position);
        }
    }
}