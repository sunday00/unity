using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2Scripts
{
    public class PlayerBall : MonoBehaviour
    {
        public GameManagerLogic gameManagerLogic;
        
        private Rigidbody _rb;
        private AudioSource _audio;
        
        private int _isJump;
        public int jumpForce;
        
        public int itemCount;
        
        
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

            if (transform.position.y < -10)
            {
                SceneManager.LoadScene(
                    "Scenes/SampleScene_" 
                    + this.gameManagerLogic.stage.ToString()
                );
            }
        }

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Box") )
            {
                this._isJump = 0;
            }
        }
        
        public void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Item":
                    this.itemCount++;
                    this._audio.Play();
                
                    other.gameObject.SetActive(false);    
                    break;
                
                case "Finish":
                    if (this.gameManagerLogic.totalItemCount == this.itemCount)
                    {
                        this.gameManagerLogic.stage++;
                    }
                    
                    SceneManager.LoadScene(
                        "Scenes/SampleScene_" 
                        + this.gameManagerLogic.stage.ToString()
                    );

                    break;
            }
        }
    }    
}
       