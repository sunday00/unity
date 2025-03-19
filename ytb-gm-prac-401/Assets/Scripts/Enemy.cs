using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int health;
    public Sprite[] sprites;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        rb.linearVelocity = Vector2.down * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "BulletBorder")
        {
            Destroy(gameObject);
            return;
        }

        if (other.tag.Equals("Bullet"))
        {
            OnHit(other.gameObject.GetComponent<Bullet>().dmg);
            Destroy(other.gameObject);
        }
    }

    private void OnHit(int damage)
    {
        health -= damage;
        sr.sprite = sprites[1];
        Invoke("ReturnSprite", 0.2f);

        if (health <= 0) Destroy(gameObject);
    }

    private void ReturnSprite()
    {
        sr.sprite = sprites[0];
    }
}