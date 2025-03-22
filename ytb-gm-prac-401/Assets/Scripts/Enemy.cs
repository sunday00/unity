using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Manager manager;

    public float speed;
    public int health;
    public int score;
    public Sprite[] sprites;

    public Item[] items;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
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

    public void SetVelocity(string n)
    {
        var z = n.Equals("PointLeft") ? 45 : n.Equals("PointRight") ? -45 : 0;
        transform.Rotate(Vector3.forward * z);

        var x = n.Equals("PointLeft") ? 1 : n.Equals("PointRight") ? -1 : 0;
        rb.linearVelocity = new Vector2(x, -1 * speed);
    }

    public void OnHit(int damage)
    {
        if (health <= 0) return;

        health -= damage;
        sr.sprite = sprites[1];
        Invoke("ReturnSprite", 0.2f);

        if (health <= 0) DestroyedByPlayer();
    }

    private void ReturnSprite()
    {
        sr.sprite = sprites[0];
    }

    private void DestroyedByPlayer()
    {
        var ran = Random.Range(0, 10);
        if (ran <= 3) Instantiate(items[ran], transform.position, Quaternion.identity);

        Destroy(gameObject);
        manager.score += score;
    }
}