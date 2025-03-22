using UnityEngine;

public class Bg : MonoBehaviour
{
    public float speed;
    public float viewHeight;

    private void Awake()
    {
        viewHeight = Camera.main.orthographicSize * 2;
    }

    private void Update()
    {
        transform.position += Vector3.down * (speed * Time.deltaTime);

        if (transform.position.y < -1.2f * viewHeight) transform.position = Vector3.up * (viewHeight * 1.8f);
    }
}