using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public float maxFireRate;
    public float curFireRate;

    public Bullet bulletA;
    public Bullet bulletB;

    public Player player;
    public ObjectManager objectManager;

    private void Update()
    {
        Fire();
        Reload();
    }

    private void Fire()
    {
        if (curFireRate < maxFireRate) return;
        if (name.Contains("EnemyS")) return;

        if (name.Contains("EnemyL"))
        {
            SpawnBullets("enemyBulletB", Vector3.right * 0.3f);
            SpawnBullets("enemyBulletB", Vector3.left * 0.3f);
        }
        else
        {
            SpawnBullets("enemyBulletA", Vector3.zero);
        }

        curFireRate = 0;
    }

    private void SpawnBullets(string bulletName, Vector3 offsetModifier)
    {
        // var bullet = Instantiate(
        //     bulletType,
        //     transform.position + offsetModifier,
        //     Quaternion.identity
        // );

        var bullet = objectManager.MakeObject(bulletName);
        bullet.transform.position = transform.position + offsetModifier;
        bullet.transform.rotation = Quaternion.identity;


        bullet.GetComponent<Rigidbody2D>().AddForce(
            (player.transform.position - (
                transform.position + offsetModifier
            )).normalized * 4,
            ForceMode2D.Impulse
        );
    }

    private void Reload()
    {
        curFireRate += Time.deltaTime;
    }
}