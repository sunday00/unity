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
        if (!gameObject.activeSelf) return;

        for (var i = 0; i < 4; i++)
            SpawnBullets(
                "bossBulletA",
                Vector3.right * (-1.5f + i * 1f),
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
        if (!gameObject.activeSelf) return;

        for (var i = 0; i < 5; i++)
            SpawnBullets(
                "bossBulletB",
                new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f), 0),
                (player.transform.position - transform.position).normalized * 3
            );

        if (_shotCount < 5)
        {
            _shotCount++;
            Invoke("FireSpread", 3.5f);
        }
        else
        {
            Invoke("FireArc", 3f);
        }
    }

    private void FireSpread()
    {
        if (!gameObject.activeSelf) return;

        SpawnBullets(
            "bossBulletB",
            Vector3.zero,
            new Vector2(Mathf.Sin(_shotCount), -1).normalized * 5
        );

        if (_shotCount < 104)
        {
            _shotCount++;
            Invoke("FireSpread", 0.2f);
        }
        else
        {
            Invoke("FireArc", 3f);
        }
    }

    private void FireArc()
    {
        if (!gameObject.activeSelf) return;

        SpawnBullets(
            "bossBulletA",
            Vector3.zero,
            new Vector2(Mathf.Sin(Mathf.PI * 10 * _shotCount / 100f), -1).normalized * 5
        );

        if (_shotCount < 203)
        {
            _shotCount++;
            Invoke("FireArc", 0.2f);
        }
        else
        {
            Invoke("FireAround", 3f);
        }
    }

    private void FireAround()
    {
        if (!gameObject.activeSelf) return;

        var max = _shotCount % 2 == 0 ? 40 : 37;
        for (var i = 0; i < max; i++)
        {
            var bullet = SpawnBullets(
                "bossBulletA",
                Vector3.zero,
                new Vector2(
                    Mathf.Cos(Mathf.PI * 2 * i / max),
                    Mathf.Sin(Mathf.PI * 2 * i / max)
                ).normalized * 2
            );

            bullet.transform.Rotate(
                Vector3.forward * 360 * i / max + Vector3.forward * 90
            );
        }

        if (_shotCount < 208)
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

    private GameObject SpawnBullets(string bulletName, Vector3 offsetModifier, Vector2 force)
    {
        var bullet = objectManager.MakeObject(bulletName);
        bullet.transform.position = transform.position + offsetModifier;
        bullet.transform.rotation = Quaternion.identity;

        bullet.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        return bullet;
    }
}