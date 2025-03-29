using UnityEngine;

namespace Ducks.Back
{
    public class MainCameraMove : MonoBehaviour
    {
        public Transform moveTarget;
        public Vector3 moveOffset;

        private void Update()
        {
            transform.position = moveTarget.position + moveOffset;
        }
    }
}