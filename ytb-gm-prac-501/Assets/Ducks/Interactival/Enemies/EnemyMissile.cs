using UnityEngine;

namespace Ducks.Interactival.Enemies
{
    public class EnemyMissile : MonoBehaviour
    {
        private void Update()
        {
            Launch();
        }

        private void Launch()
        {
            transform.Rotate(Vector3.right * 60 * Time.deltaTime);
        }
    }
}