using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Manager manager;
    public int speed;
    public Vector3 direction = Vector3.down;
    private Animator _animator;

    private GameObject _facialObject;

    private float _h;

    private float _mh;
    private float _mv;

    private Rigidbody2D _rigid;
    private float _v;


    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _h = Input.GetAxisRaw("Horizontal") + _mh;
        _v = Input.GetAxisRaw("Vertical") + _mv;

        var hDirection = _animator.GetInteger(manager.playerAnimatorProps.hAxisRaw);
        var vDirection = _animator.GetInteger(manager.playerAnimatorProps.vAxisRaw);

        Facial(hDirection, vDirection);

        if (Input.GetButtonDown("Interact")) Interact();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void MuiTouch(string key)
    {
        switch (key)
        {
            case "ud":
                _mv = 1;
                break;
            case "ld":
                _mh = -1;
                break;
            case "rd":
                _mh = 1;
                break;
            case "dd":
                _mv = -1;
                break;

            case "uu":
            case "du":
                _mv = 0;
                break;
            case "lu":
            case "ru":
                _mh = 0;
                break;

            case "ad":
                Interact();
                break;
            // case "au": break;

            case "ed":
                manager.SubmenuSet.SetActive(!manager.SubmenuSet.activeSelf);
                break;

            // case "eu": break;
        }
    }

    private void Facial(int hDirection, int vDirection)
    {
        if (manager.talkPanel.GetBool("isShow")) return;

        // set animation direction
        if (!hDirection.Equals((int)_h))
        {
            _animator.SetInteger(manager.playerAnimatorProps.hAxisRaw, (int)_h);
            _animator.SetBool(manager.playerAnimatorProps.isWalk, true);
        }

        else if (!vDirection.Equals((int)_v))
        {
            _animator.SetInteger(manager.playerAnimatorProps.vAxisRaw, (int)_v);
            _animator.SetBool(manager.playerAnimatorProps.isWalk, true);
        }

        else
        {
            _animator.SetBool(manager.playerAnimatorProps.isWalk, false);
        }

        // get and set facial direction
        if (hDirection != 0 || vDirection != 0) direction = new Vector3(hDirection, vDirection, 0);
    }

    private void Move()
    {
        if (manager.talkPanel.GetBool("isShow")) return;

        // Player move
        var movement = _h != 0 ? new Vector2(_h, 0) : new Vector2(0, _v);
        _rigid.linearVelocity = movement * speed;

        Debug.DrawRay(_rigid.position, direction * 0.75f, Color.red);
        var rayHit = Physics2D.Raycast(_rigid.position, direction, 0.75f, LayerMask.GetMask("Interactive"));

        _facialObject = rayHit.collider?.gameObject;
    }

    private void Interact()
    {
        if (_facialObject.IsUnityNull()) return;

        manager.Interact(_facialObject);
    }
}