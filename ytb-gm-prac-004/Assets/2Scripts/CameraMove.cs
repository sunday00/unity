using UnityEngine;

namespace _2Scripts
{
    public class CameraMove : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset;

        public void Awake()
        {
            this.player = GameObject.FindGameObjectWithTag("Player").transform;
            this.offset = transform.position - player.position;
        }

        public void LateUpdate()
        {
            transform.position = this.player.position + this.offset; 
        }
    }
    
}

