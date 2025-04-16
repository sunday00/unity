using UnityEngine;

namespace Ducks.Interactival.Items
{
    public class BulletAction : MonoBehaviour
    {
        public int damage;

        protected void OnCollisionEnter(Collision other)
        {
            switch (other.gameObject.tag)
            {
                case "Floor":
                    if (gameObject.name.Equals("BossRock")) return;

                    Destroy(gameObject, 3);
                    break;
                case "Wall":
                    Destroy(gameObject);
                    break;
            }
        }
    }
}