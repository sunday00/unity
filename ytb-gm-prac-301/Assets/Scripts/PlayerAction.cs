using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private float _h;
    private float _v;

    private Rigidbody2D _rigid;

    void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        _rigid.linearVelocity = new Vector2(_h * 10, _v * 10);
    }
}
