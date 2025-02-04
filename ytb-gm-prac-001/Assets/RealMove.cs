using System;
using UnityEngine;

public class RealMove : MonoBehaviour
{
    public void Start()
    {
        // Vector3 mv = new Vector3(0.2f, 0, 0);
        // transform.Translate(mv);
    }

    public void Update()
    {
        float y = 0f;
        if (Input.GetButtonDown("Jump")) y = 1;
        else if (Input.GetButtonUp("Jump")) y = -1;

        // Vector3 mv = new Vector3(
        //     0.01f * Input.GetAxisRaw("Horizontal"),
        //     y, 
        //     0.01f * Input.GetAxisRaw("Vertical")
        // );
        
        Vector3 mv = new Vector3(
            // apply cpu frame time. 
            // when online game, make same move between fast frame and drop frame
            Time.deltaTime * 4f * Input.GetAxisRaw("Horizontal"),
            y, 
            Time.deltaTime * 4f * Input.GetAxisRaw("Vertical")
        );
            
        transform.Translate(mv);
    }
}
