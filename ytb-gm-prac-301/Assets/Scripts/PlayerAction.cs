using Unity.VisualScripting;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public Manager manager;
    
    private float _h;
    private float _v;
    public int speed;
    public Vector3 direction = Vector3.down;

    private Rigidbody2D _rigid;
    private Animator _animator;

    private GameObject _facialObject;
    
    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();   
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        int hDirection = _animator.GetInteger("hAxisRaw");
        int vDirection = _animator.GetInteger("vAxisRaw");
        
        this.Facial(hDirection, vDirection);

        if (Input.GetButtonDown("Interact"))
        {
            this.Interact();
        }
    }

    void FixedUpdate()
    {
        this.Move();
    }

    void Facial(int hDirection, int vDirection)
    {
        if (manager.talkPanel.activeSelf) return;
        
        // set animation direction
        if(hDirection != _h)
        {
            _animator.SetInteger("hAxisRaw", (int)_h);
            _animator.SetBool("isWalk", true);
        }
        
        else if(vDirection != _v)
        {
            _animator.SetInteger("vAxisRaw", (int)_v);
            _animator.SetBool("isWalk", true);
        }

        else 
        {
            _animator.SetBool("isWalk", false);    
        }
        
        // get and set facial direction
        if (hDirection != 0 || vDirection != 0)
        {
            direction = new Vector3( hDirection, vDirection, 0 );
        }
    }

    void Move()
    {
        if (manager.talkPanel.activeSelf) return;
        
        // Player move
        Vector2 movement = _h != 0 ? new Vector2(_h, 0) : new Vector2(0, _v);
        _rigid.linearVelocity =  movement * speed;
        
        Debug.DrawRay(_rigid.position, direction * 0.75f, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(_rigid.position, direction, 0.75f, LayerMask.GetMask("Interactive"));

        _facialObject = rayHit.collider?.gameObject;
    }

    void Interact()
    {
        if (_facialObject.IsUnityNull()) return;
        
        manager.Interact(_facialObject);
    }
}
