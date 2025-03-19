using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletCount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("BulletBorder")) Destroy(gameObject);
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
}