using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float s;

    private Animator _animator;

    private Dictionary<string, bool> _blockedPos;

    private float _h;

    private bool _isJoyPoint;
    private Vector2 _joyVector;
    private SpriteRenderer _spriteRenderer;
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
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
    }

    private void OnEnable()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("SetEnableFull", 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Border")) BlockByBorder(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Border")) AwayFromBorder(other);
    }

    private void SetEnableFull()
    {
        _spriteRenderer.color = new Color(1, 1, 1, 1);
        GetComponent<PlayerHit>().isHit = false;
    }

    private void Move()
    {
        _h = Input.GetAxisRaw("Horizontal") + (_isJoyPoint ? _joyVector.x : 0);
        _v = Input.GetAxisRaw("Vertical") + (_isJoyPoint ? _joyVector.y : 0);

        _animator.SetInteger(Constants.PlayerAniInputH, (int)_h);

        if ((_v > 0 && _blockedPos["Top"]) || (_v < 0 && _blockedPos["Bottom"])) _v = 0;
        if ((_h < 0 && _blockedPos["Left"]) || (_h > 0 && _blockedPos["Right"])) _h = 0;


        var currentPos = transform.position;
        var velocity = s * Time.deltaTime;
        var targetPos = new Vector3(_h * velocity, _v * velocity, 0);

        transform.position = currentPos + targetPos;
    }

    public void JoyPanel(string type)
    {
        var h = 0f;
        var v = 0f;

        if (type.Contains("u")) v = 1f;
        if (type.Contains("b")) v = -1f;
        if (type.Contains("l")) h = -1f;
        if (type.Contains("r")) h = 1f;

        _joyVector = new Vector2(h, v);
    }

    public void JoyActive(bool active)
    {
        _isJoyPoint = active;
    }

    private void BlockByBorder(Collider2D other)
    {
        _blockedPos[other.gameObject.name] = true;
    }

    private void AwayFromBorder(Collider2D other)
    {
        _blockedPos[other.gameObject.name] = false;
    }
}