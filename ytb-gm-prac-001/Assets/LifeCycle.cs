using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    void Awake () 
    {
        Debug.Log("awake");
    }

    void OnEnable ()
    {
        Debug.Log("activated");
    }

    void Start () 
    {
        Debug.Log("start");
    }

    // using cpu, looping infinite much.
    void FixedUpdate()
    {
        Debug.Log("move");
    }

    // using cpu, looping infinite much.
    void Update()
    {
        Debug.Log("hunt");
    }

    // using cpu, looping infinite much.
    void LateUpdate()
    {
        Debug.Log("follow camera");
    }

    void OnDisable() 
    {
        Debug.Log("inactivated");
    }

    void OnDestroy()
    {
        Debug.Log("removed");
    }
}
