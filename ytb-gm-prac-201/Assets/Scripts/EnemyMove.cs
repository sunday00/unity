using Unity.VisualScripting;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public int nextMove;
    
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        this.Think();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.linearVelocity = new Vector2(this.nextMove, rigid.linearVelocity.y);

        Vector2 frontVec = new Vector2(rigid.position.x + (nextMove * 0.3f), rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, Color.green);
        RaycastHit2D rayHit = Physics2D.Raycast(
            frontVec, Vector3.down, 1, LayerMask.GetMask("Floor")
        );
        if (rayHit.collider.IsUnityNull())
        {
            CancelInvoke(nameof(Think));
            nextMove *= -1;
            
            Flipper();
        }
    }

    void Think()
    {
        nextMove = Random.Range(-1, 2);
        
        Flipper();
    }

    void Flipper()
    {
        animator.SetInteger("walkSpeed", nextMove);
        if (!nextMove.Equals(0)) spriteRenderer.flipX = nextMove > 0;
        
        Invoke(nameof(Think), 5);
    }
}
