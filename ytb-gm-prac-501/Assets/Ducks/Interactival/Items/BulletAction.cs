using UnityEngine;

namespace Ducks.Interactival.Items
{
    public class BulletAction : MonoBehaviour
    {
        public int damage;

        private void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Floor":
                    Destroy(gameObject, 3);
                    break;
                case "Wall":
                    Destroy(gameObject);
                    break;
            }
        }
    }
}