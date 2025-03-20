using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public float maxFireRate;
    public float curFireRate;

    public Bullet bulletA;
    public Bullet bulletB;

    public Player player;

    private void Update()
    {
        Fire();
        Reload();
    }

    private void Fire()
    {
        if (curFireRate < maxFireRate) return;
        if (name.Contains("EnemyS")) return;

        var bullet = Instantiate(
            name.Contains("EnemyL") ? bulletA : bulletB,
            transform.position,
            Quaternion.identity
        );

        curFireRate = 0;
    }

    private void Reload()
    {
        curFireRate += Time.deltaTime;
    }
}