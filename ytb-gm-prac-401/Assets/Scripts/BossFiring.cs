using UnityEngine;

public class BossFiring : MonoBehaviour
{
    public Player player;
    public ObjectManager objectManager;

    private int _shotCount;
    private bool _start;

    private void Update()
    {
        if (!_start && transform.position.y <= 3f)
        {
            _start = true;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            Invoke("FireForward", 2f);
        }
    }

    private void FireForward()
    {
        for (var i = 0; i < 4; i++)
            SpawnBullets(
                "bossBulletA",
                Vector3.left * (-0.3f + i * 1.5f),
                Vector2.down * 4
            );

        if (_shotCount < 2)
        {
            _shotCount++;
            Invoke("FireForward", 2f);
        }
        else
        {
            Invoke("FireShot", 3f);
        }
    }

    private void FireShot()
    {
        print("FireShot");

        if (_shotCount < 5)
        {
            _shotCount++;
            Invoke("FireShot", 3.5f);
        }
        else
        {
            Invoke("FireArc", 3f);
        }
    }

    private void FireArc()
    {
        print("FireArc");

        if (_shotCount < 20)
        {
            _shotCount++;
            Invoke("FireArc", 1.5f);
        }
        else
        {
            Invoke("FireAround", 3f);
        }
    }

    private void FireAround()
    {
        print("FireAround");

        if (_shotCount < 25)
        {
            _shotCount++;
            Invoke("FireAround", 1f);
        }
        else
        {
            _shotCount = 0;
            Invoke("FireForward", 3f);
        }
    }

    private void SpawnBullets(string bulletName, Vector3 offsetModifier, Vector2 force)
    {
        var bullet = objectManager.MakeObject(bulletName);
        bullet.transform.position = transform.position + offsetModifier;
        bullet.transform.rotation = Quaternion.identity;


        bullet.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }
}