using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    
    public Rigidbody2D rb;
    public float maxSpeed;  
    public Color originalColor;
    public Collider2D col;

    public float jumpForce;
    public int jumpCount = 0;
    
    public SpriteRenderer sr;
    public Animator animator;
    
    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip itemSound;
    
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        originalColor = sr.color;
        audioSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            this.sr.flipX = Input.GetAxisRaw("Horizontal") < 0;
        }
        
        if (Input.GetButtonUp("Horizontal"))
        {
            rb.linearVelocity = new Vector2(
                0.5f * rb.linearVelocity.normalized.x, 
                rb.linearVelocityY
            );   
        }
        
        animator.SetBool("isWalking", Mathf.Abs(rb.linearVelocityX) > 0.3f);
        
        // jump

        if (Input.GetButtonDown("Jump") || (Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0))
        {
            if (jumpCount > 2) return;
            
            animator.SetBool("isJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
            audioSource.PlayOneShot(jumpSound);
        }
    }

    public void FixedUpdate()
    {
        rb.AddForce(Vector2.right * Input.GetAxis("Horizontal"), ForceMode2D.Impulse);
        
        if (Mathf.Abs(rb.linearVelocityX) > maxSpeed)
        {
            rb.linearVelocity = new Vector2(maxSpeed * rb.linearVelocity.normalized.x, rb.linearVelocityY);
        }
        
        // de Jump
        Debug.DrawRay(rb.position, Vector3.down, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(
            rb.position, Vector3.down, 1, LayerMask.GetMask("Floor")
        );
        if (rayHit.collider != null && rayHit.distance < 0.5f && rb.linearVelocity.y < 0)
        {
            // print(rayHit.collider.name);
            animator.SetBool("isJumping", false);
            jumpCount = 0;
        }
    }

    public void LateUpdate()
    {
        if (rb.position.y < -10)
        {
            // SceneManager.LoadScene("Scenes/S0");
            OnFallen();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (rb.linearVelocityY < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision);
                return;
            }
            
            OnDamaged(collision.transform.position);
        }
    }

    public void OnDamaged(Vector2 targetPos)
    {
        gameManager.PlayerDamaged();
        
        gameObject.layer = 16;
        sr.color = new Color(1, 1, 1, 0.4f);

        rb.AddForce(new Vector2(transform.position.x - targetPos.x > 0 ? 1: -1, 1) * 10, ForceMode2D.Impulse);
        
        animator.SetTrigger("damaged");
        
        Invoke("Recover", 3);
    }
    
    public void OnFallen()
    {
        gameManager.PlayerDamaged();
        
        gameObject.layer = 16;
        sr.color = new Color(1, 1, 1, 0.4f);

        gameManager.Respawn();  
        
        animator.SetTrigger("damaged");
        
        Invoke("Recover", 3);
    }

    public void Recover()
    {
        gameObject.layer = 15;
        sr.color = originalColor;
    }
    
    public void OnAttack(Collision2D collision)
    {
        gameManager.stagePoints += 100;
        
        rb.AddForce(Vector2.up * 15f, ForceMode2D.Impulse);
        EnemyMove em = collision.transform.GetComponent<EnemyMove>();
        em.OnDamaged();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            collision.gameObject.SetActive(false);
            gameManager.stagePoints += 100;
            audioSource.PlayOneShot(itemSound);
        }

        if (collision.gameObject.CompareTag("Finish"))
        {
            // Next stage
            gameManager.NextStage();
        }
    }

    public void OnDeath()
    {
        sr.color = new Color(1f, 1f, 1f, 0.4f);
        sr.flipY = true;
        col.enabled = false;
        rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
    }
}
