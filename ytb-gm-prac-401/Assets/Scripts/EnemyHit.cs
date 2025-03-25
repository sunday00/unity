using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public Manager manager;

    public int health;
    public int initialHealth;
    public float hitSize;

    public Sprite[] sprites;

    public int score;
    private string[] _items;
    private SpriteRenderer _sr;

    private void Awake()
    {
        _items = new[] { "itemPower", "itemCoin", "itemBomb", "itemLife" };
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        health = initialHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Bullet"))
        {
            OnHit(other.gameObject.GetComponent<Bullet>().dmg);
            other.gameObject.SetActive(false);
        }
    }

    public void OnHit(int damage)
    {
        if (health <= 0) return;

        health -= damage;
        _sr.sprite = sprites[1];
        Invoke("ReturnSprite", 0.2f);

        if (health <= 0) DestroyedByPlayer();
    }

    private void ReturnSprite()
    {
        _sr.sprite = sprites[0];
    }

    private void DestroyedByPlayer()
    {
        var ran = Random.Range(0, 10);
        if (ran <= 3)
        {
            var item = manager.objectManager.MakeObject(_items[ran]);
            item.transform.position = transform.position;
            item.transform.rotation = Quaternion.identity;
        }

        gameObject.SetActive(false);
        manager.CallExplosion(transform.position, hitSize);
        manager.score += score;
    }
}