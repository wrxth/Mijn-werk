using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreVisited : MonoBehaviour
{
 
    public bool Visited;
    public GameObject LocationPoint;
    public GameObject Camera;
    public LayerMask astoridmask;
	
    private void OnEnable()
    {
        Visited = false;
        transform.rotation = Quaternion.Euler(90,0,0);
    }

    private void Update()
    {
        var ast = Physics.OverlapSphere(transform.position, 100, astoridmask);
        for (int i = 0; i < ast.Length; i++)
        {
            if(ast[i] != null)
                ast[i].GetComponent<Asteroid>().Push(transform.position);
        }
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,100);
    }*/
}
