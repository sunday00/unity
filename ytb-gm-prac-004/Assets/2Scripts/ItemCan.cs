using UnityEngine;

namespace _2Scripts 
{
    public class ItemCan : MonoBehaviour
    {
     
        public float rotateSpeed;
        
        void Update()
        {
            transform.Rotate(new Vector3(0, 1f * rotateSpeed * Time.deltaTime, 0), Space.World);
        }
    }
    
}
