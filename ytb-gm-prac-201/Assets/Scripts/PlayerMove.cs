using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public float maxSpeed;  

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.linearVelocity = new Vector2(
                0.5f * rb.linearVelocity.normalized.x, 
                rb.linearVelocityY
            );   
        }
    }

    public void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");  
        rb.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // if (this.rb.velocity.x > maxSpeed)
        if (Math.Abs(rb.linearVelocityX) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(maxSpeed * (rb.linearVelocityX >= 0 ? 1 : -1), rb.linearVelocityY);
        }
        // else if (this.rb.linearVelocityX < maxSpeed * -1)
        // {
        //     this.rb.linearVelocity = new Vector2(maxSpeed * -1, rb.linearVelocityY);
        // }
    }

    public void LateUpdate()
    {
        if (rb.position.y < -10)
        {
            SceneManager.LoadScene("Scenes/S0");
        }
    }
}
