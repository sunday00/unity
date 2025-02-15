using Unity.VisualScripting;
using UnityEngine;

public class MainBall : MonoBehaviour
{
    private Rigidbody rigid;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        // rigid.linearVelocity = Vector3.right;
        // rigid.linearVelocity = new Vector3(-10, 5, 0);
        // rigid.AddForce(Vector3.up * 40, ForceMode.Impulse);
        
        rigid.AddTorque(Vector3.up * 10f);
    }
    
    void FixedUpdate()
    {
        Vector3 v = new Vector3(
            Input.GetAxis("Horizontal") * 10, 0, Input.GetAxis("Vertical") * 10
        );

        // rigid.AddForce(v.normalized * 10, ForceMode.Impulse);
        rigid.AddForce(v, ForceMode.Impulse);
        
        rigid.AddTorque(Vector3.up * 10f);
    }

    // void Update()
    // {
    //     if (Input.GetButtonDown("Jump"))
    //     { 
    //         rigid.AddForce(Vector3.up * 40, ForceMode.Impulse);
    //     }
    // }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "SubCube1")
        {
            rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }
    }

    public void OnButtonClick()
    {
        rigid.AddForce(Vector3.up * 40, ForceMode.Impulse);
    }
}
