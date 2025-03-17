using UnityEngine;

public class Player : MonoBehaviour
{
    public float _s;

    private float _h;

    private bool _isTouchBottom;
    private bool _isTouchLeft;
    private bool _isTouchRight;
    private bool _isTouchTop;

    private float _v;

    private void Awake()
    {
    }

    private void Update()
    {
        Move();
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

        if ((_v > 0 && _isTouchTop) || (_v < 0 && _isTouchBottom)) _v = 0;
        if ((_h > 0 && _isTouchRight) || (_h < 0 && _isTouchLeft)) _h = 0;

        var currentPos = transform.position;
        var velocity = _s * Time.deltaTime;
        var targetPos = new Vector3(_h * velocity, _v * velocity, 0);

        transform.position = currentPos + targetPos;
    }

    private void BlockByBorder(Collider2D other)
    {
        switch (other.name)
        {
            case "Top":
                _isTouchTop = true; break;
            case "Bottom":
                _isTouchBottom = true; break;
            case "Left":
                _isTouchLeft = true; break;
            case "Right":
                _isTouchRight = true; break;
        }
    }

    private void AwayFromBorder(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "Top":
                _isTouchTop = false; break;
            case "Bottom":
                _isTouchBottom = false; break;
            case "Left":
                _isTouchLeft = false; break;
            case "Right":
                _isTouchRight = false; break;
        }
    }
}