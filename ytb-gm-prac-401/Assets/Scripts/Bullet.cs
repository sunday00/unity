using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public int bulletCount = 1;
    public int dmg;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Equals("BulletBorder"))
            // Destroy(gameObject);
            gameObject.SetActive(false);
    }

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }
}