using System;
using Unity.Hierarchy;
using UnityEngine;

public class MoveToward : MonoBehaviour
{
    public void Start()
    {
        // Vector3 mv = new Vector3(0.2f, 0, 0);
        // transform.Translate(mv);
    }

    private Vector3 _target = new Vector3(9.57f,0.5f,0);
    
    public void Update()
    {
        // // 1. straight forward move
        // transform.position = Vector3.MoveTowards(
        //     // transform.position, target, Time.deltaTime * 10f
        //     transform.position, this._target, 1f
        // );

        
        // // 2. smoothly before reach
        // Vector3 zero = Vector3.zero;
        //
        // transform.position = Vector3.SmoothDamp(
        //     transform.position, this._target, ref zero, 0.1f
        // );

        // // 3. smoothly before reach more work well
        // transform.position = Vector3.Lerp(
        //     transform.position, this._target, 0.01f
        // );
        
        // 4. curved move
        transform.position = Vector3.Slerp(
            transform.position, this._target, 0.01f
        );
    }
}
