using UnityEngine;

namespace Duck.Player
{
    public class Dongle : MonoBehaviour
    {
        public Camera mainCamera;

        private void Start()
        {
        }

        private void Update()
        {
            // Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            transform.position = Vector2.Lerp(transform.position, mousePosition, 0.1f);
        }
    }
}