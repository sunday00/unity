using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Constants constants;

    public float s;

    public GameObject bulletA;
    public GameObject bulletB;
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

    private void Update()
    {
        Move();
        Fire();
        Reload();
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

    private void Fire()
    {
        if (!Input.GetButton("Fire1")) return;
        if (curFireRate < maxFireRate) return;

        var bullet = Instantiate(bulletA, transform.position, Quaternion.identity);
        // var bullet = Instantiate(bulletA, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curFireRate = 0;
    }

    private void Reload()
    {
        curFireRate += Time.deltaTime;
    }
}