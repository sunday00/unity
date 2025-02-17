using System;
using Unity.VisualScripting;
using UnityEngine;

namespace _2Scripts
{
    public class PlayerBall : MonoBehaviour
    {
        private Rigidbody _rb;
        private AudioSource _audio;
        
        private int _isJump = 0;
        public int jumpForce;
        
        public int itemCount = 0;
        
        
        public void Awake()
        {
            this._rb = GetComponent<Rigidbody>();
            this._audio = GetComponent<AudioSource>();

        }

        public void FixedUpdate()
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            
            this._rb.AddForce(new Vector3(h, 0, v), ForceMode.Impulse);
        }
        
        public void LateUpdate()
        {
            if (Input.GetButtonDown("Jump") && this._isJump < 2)
            {
                this._rb.AddForce(new Vector3(0, this.jumpForce ,0), ForceMode.Impulse);
                this._isJump += 1;
            }

            // if (this._rb.position.y <= 0.51f)
            // {
            //     this._isJump = 0;
            // }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor") )
            {
                this._isJump = 0;
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Item"))
            {
                this.itemCount++;
                this._audio.Play();
                
                other.gameObject.SetActive(false);
            }
        }
    }    
}
       