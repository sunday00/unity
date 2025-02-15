using System;
using UnityEngine;

public class SubSphere1Move1 : MonoBehaviour
{
    private MeshRenderer mesh;
    Material mat;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = mesh.material;
        
    }

    void OnCollisionEnter(Collision other)
    {
        // if (other.gameObject.CompareTag("Player"))
        if (other.gameObject.name == "MainSphere")
        {
            mat.color = new Color(0f, 0f, 0f, 1f);
        }
    }

    private void OnCollisionStay(Collision other)
    {
        // throw new NotImplementedException();
    }

    void OnCollisionExit(Collision collision)
    {
        mat.color = new Color(1f, 1f, 1f, 1f);
    }
}
