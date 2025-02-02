using System;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            print("Currently moving");
            // print(transform.position.x);
            // print(Input.GetAxis("Horizontal"));
            print(Input.GetAxisRaw("Horizontal"));
        }
    }
}
