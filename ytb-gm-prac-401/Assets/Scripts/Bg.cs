using UnityEngine;

public class Bg : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;

        if (transform.position.y < -12) transform.position = Vector3.up * 18;
    }
}