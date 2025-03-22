using System;
using UnityEngine;

public class PlayerFiring : MonoBehaviour
{
    public float maxFireRate;
    public float curFireRate;

    public Bullet curBullet;
    public Bullet subBullet;

    public ObjectManager objectManager;

    private PlayerItem pItem;

    private void Awake()
    {
        pItem = GetComponent<PlayerItem>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2")) SwitchWeapon();
        if (Input.GetButtonDown("Fire3")) pItem.EnableBoom();
        Fire();
        Reload();
    }

    private void Fire()
    {
        if (!Input.GetButton("Fire1")) return;
        if (curFireRate < maxFireRate) return;

        // center fire
        // var bullet = Instantiate(curBullet, transform.position, Quaternion.identity);
        var bullet = objectManager.MakeObject(curBullet.name);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        // sub fire
        var harf = Math.Floor((double)curBullet.bulletCount / 2);
        for (var i = -1 * harf; i <= harf; i++)
        {
            if (i.Equals(0)) continue;

            // var bulletSide = Instantiate(curBullet, transform.position, Quaternion.Euler(0, 0, (float)(30 * -i)));
            var bulletSide = objectManager.MakeObject(curBullet.name);
            bulletSide.transform.position = transform.position;
            bulletSide.transform.rotation = Quaternion.Euler(0, 0, (float)(30 * -i));
            bulletSide.GetComponent<Rigidbody2D>()
                .AddForce(new Vector2((float)i * 0.5f, 1).normalized * 10, ForceMode2D.Impulse);
        }

        curFireRate = 0;
    }

    private void SwitchWeapon()
    {
        var _ = curBullet;
        curBullet = subBullet;
        subBullet = _;

        maxFireRate = curBullet.GetBulletSpeed();
    }


    private void Reload()
    {
        curFireRate += Time.deltaTime;
    }
}