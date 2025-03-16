using UnityEngine;

public class Player : MonoBehaviour
{
    public float _s;
    private float _h;
    private float _v;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        _h = Input.GetAxisRaw("Horizontal");
        _v = Input.GetAxisRaw("Vertical");

        var currentPos = transform.position;
        var velocity = _s * Time.deltaTime;
        var targetPos = new Vector3(_h * velocity, _v * velocity, 0);

        transform.position = currentPos + targetPos;
    }
}