using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public int nextMove;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        this.Think();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(this.nextMove, rigid.linearVelocity.y);
    }

    void Think()
    {
        this.nextMove = Random.Range(-1, 2);
        
        Invoke(nameof(Think), 5);
    }
}
