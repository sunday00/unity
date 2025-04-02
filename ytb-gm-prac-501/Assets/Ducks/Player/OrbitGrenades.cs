using UnityEngine;

namespace Ducks.Player
{
    public class OrbitGrenades : MonoBehaviour
    {
        public Transform target;
        public float orbitSpeed;

        private void Update()
        {
            Orbit();
        }

        private void Orbit()
        {
            transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}