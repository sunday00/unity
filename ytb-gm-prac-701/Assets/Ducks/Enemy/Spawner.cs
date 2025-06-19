using UnityEngine;

namespace Ducks.Enemy
{
    public class Spawner : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetButtonDown("Jump")) GameManager.Instance.pool.Get(0);
        }
    }
}