using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private float _h;
    private float _v;
    public int speed;
    public Vector3 direction;

    private Rigidbody2D _rigid;
    private Animator _animator;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();   
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
        
        if(_animator.GetInteger("hAxisRaw") != _h)
        {
            _animator.SetInteger("hAxisRaw", (int)_h);
            _animator.SetBool("isWalk", true);
        }
        
        else if(_animator.GetInteger("vAxisRaw") != _v)
        {
            _animator.SetInteger("vAxisRaw", (int)_v);
            _animator.SetBool("isWalk", true);
        }

        else 
        {
            _animator.SetBool("isWalk", false);    
        }
        
    }

    void FixedUpdate()
    {
        Vector2 movement = _h != 0 ? new Vector2(_h, 0) : new Vector2(0, _v);
        _rigid.linearVelocity =  movement * speed;
    }
}
