using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Constants constants;

    public float s;

    public Bullet curBullet;
    public Bullet subBullet;

    public float maxFireRate;
    public float curFireRate;

    private Animator _animator;

    private Dictionary<string, bool> _blockedPos;

    private float _h;
    private float _v;

    private void Awake()
    {
        _blockedPos = new Dictionary<string, bool>
        {
            { "Top", false },
            { "Bottom", false },
            { "Left", false },
            { "Right", false }
        };

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire2")) SwitchWepon();

        Move();
        Fire();
        Reload();
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Border")) BlockByBorder(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Border")) AwayFromBorder(other);
    }

    private void Move()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        _animator.SetInteger(constants.playerAniInputH, (int)_h);

        if ((_v > 0 && _blockedPos["Top"]) || (_v < 0 && _blockedPos["Bottom"])) _v = 0;
        if ((_h < 0 && _blockedPos["Left"]) || (_h > 0 && _blockedPos["Right"])) _h = 0;


        var currentPos = transform.position;
        var velocity = s * Time.deltaTime;
        var targetPos = new Vector3(_h * velocity, _v * velocity, 0);

        transform.position = currentPos + targetPos;
    }

    private void BlockByBorder(Collider2D other)
    {
        _blockedPos[other.gameObject.name] = true;
    }

    private void AwayFromBorder(Collider2D other)
    {
        _blockedPos[other.gameObject.name] = false;
    }

    private void SwitchWepon()
    {
        var _ = curBullet;
        curBullet = subBullet;
        subBullet = _;

        maxFireRate = curBullet.GetBulletSpeed();
    }

    private void Fire()
    {
        if (!Input.GetButton("Fire1")) return;
        if (curFireRate < maxFireRate) return;

        // center fire
        var bullet = Instantiate(curBullet, transform.position, Quaternion.identity);
        // var bullet = Instantiate(bulletA, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        // sub fire
        var harf = Math.Floor((double)curBullet.bulletCount / 2);
        for (var i = -1 * harf; i <= harf; i++)
        {
            if (i.Equals(0)) continue;

            var bulletSide = Instantiate(curBullet, transform.position, Quaternion.Euler(0, 0, (float)(30 * -i)));
            bulletSide.GetComponent<Rigidbody2D>()
                .AddForce(new Vector2((float)i * 0.5f, 1).normalized * 10, ForceMode2D.Impulse);
        }
        
        curFireRate = 0;
    }

    private void Reload()
    {
        curFireRate += Time.deltaTime;
    }
}