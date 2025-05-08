using UnityEngine;

namespace Duck.Player
{
    public class Dongle : MonoBehaviour
    {
        public Camera mainCamera;
        public bool isDrag;
        private Rigidbody2D _rb;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!isDrag) return;

            // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            var leftBorder = -3.9f + transform.localScale.x / 2f;
            var rightBorder = 3.9f - transform.localScale.x / 2f;

            if (mousePosition.x < leftBorder) mousePosition.x = leftBorder;
            if (mousePosition.x > rightBorder) mousePosition.x = rightBorder;

            mousePosition.y = 8f;

            transform.position = Vector2.Lerp(transform.position, mousePosition, 0.2f);
        }

        public void Drag()
        {
            isDrag = true;
        }

        public void Drop()
        {
            isDrag = false;
            _rb.simulated = true;
        }
    }
}