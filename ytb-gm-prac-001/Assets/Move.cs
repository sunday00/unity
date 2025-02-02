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
    }
}
