using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private Vector3 zero = Vector3.zero;
    private Vector3 v1, v2, v3;
    private float angle;

    void Start()
    {
        v1 = new Vector3(2.5f, 2.5f, 2.5f);
        v2 = new Vector3(1f, 0f, 1f);
        v3 = new Vector3(1f, 0f, 1f);
        angle = Vector3.Angle(v1, v3);
    }
    
    void Update()
    {
        
    }

    // Show a Scene view example.
    void OnDrawGizmosSelected()
    {
        // Left/right and up/down axes.
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position - new Vector3(2.25f, 0, 0), transform.position + new Vector3(2.25f, 0, 0));
        Gizmos.DrawLine(transform.position - new Vector3(0, 1.75f, 0), transform.position + new Vector3(0, 1.75f, 0));

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(zero, v1);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(zero, v2);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(zero, v3);
    }
}