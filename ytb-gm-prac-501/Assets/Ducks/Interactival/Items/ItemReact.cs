using UnityEngine;

namespace Ducks.Interactival.Items
{
    public class ItemReact : MonoBehaviour
    {
        public Constants.Type type;
        public int value;

        private void Update()
        {
            transform.Rotate(Vector3.up * 50f * Time.deltaTime);
        }
    }
}