using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "BulletBorder") gameObject.SetActive(false);
    }

    public void SetVelocity(string n)
    {
        var z = n.Equals("PointLeft") ? 45 : n.Equals("PointRight") ? -45 : 0;
        transform.Rotate(Vector3.forward * z);

        var x = n.Equals("PointLeft") ? 1 : n.Equals("PointRight") ? -1 : 0;
        _rb.linearVelocity = new Vector2(x, -1 * speed);
    }

    public void SetBossVelocity()
    {
        _rb.linearVelocity = Vector2.down * speed;
    }
}