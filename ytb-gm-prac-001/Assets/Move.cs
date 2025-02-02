using UnityEngine;

public class Move : MonoBehaviour
{
    // using cpu, looping infinite much.
    void Update()
    {
        if (Input.anyKeyDown)
        {
            print("Ohh");
        }
        
        if (Input.anyKey)
        {
            print("keep pressing...");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            print("left down");
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            print("left down still");
        }
        
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            print("left up");
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            print("done");
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            print("click");
        }
        
        if (Input.GetMouseButton(0))
        {
            print("click");
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            print("click");
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            print("click1");
        }
        
        if (Input.GetMouseButton(1))
        {
            print("click1");
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            print("click1");
        }
        
        // Look UnityEditor/Edit/ProjectSetting/InputManager/Axes
        if (Input.GetButtonDown("Jump"))
        {
            print("jump prepare");
        }
        
        if (Input.GetButton("Jump"))
        {
            print("jump save");
        }
        
        if (Input.GetButtonUp("Jump"))
        {
            print("jump!");
        }
        
        if (Input.GetButton("Fire1"))
        {
            print("fire");
        }
        
        if (Input.GetButton("Boo"))
        {
            print("booo");
        }
    }
}
