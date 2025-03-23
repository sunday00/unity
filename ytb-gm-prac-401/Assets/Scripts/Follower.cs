using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float maxFireRate;
    public float curFireRate;

    public Bullet curBullet;

    public ObjectManager objectManager;

    public Vector3 followPos;
    public int followDelay;
    public Transform parent;
    public Queue<Vector3> parentPos;

    private void Awake()
    {
        parentPos = new Queue<Vector3>();
    }

    private void Update()
    {
        Watch();
        Follow();
        Fire();
        Reload();
    }

    private void Watch()
    {
        if (parentPos.Contains(parent.position)) return;

        parentPos.Enqueue(parent.position);

        if (parentPos.Count > followDelay) followPos = parentPos.Dequeue();
        else followPos = new Vector3(parent.position.x + 1, parent.position.y, parent.position.z);
    }

    private void Follow()
    {
        transform.position = followPos;
    }

    private void Fire()
    {
        if (curFireRate < maxFireRate) return;

        var bullet = objectManager.MakeObject(curBullet.name);
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        curFireRate = 0;
    }

    private void Reload()
    {
        curFireRate += Time.deltaTime;
    }
}