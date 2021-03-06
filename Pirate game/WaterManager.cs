using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    private MeshFilter Mesh;

    private void Awake()
    {
        Mesh = GetComponent<MeshFilter>();
    }

    private void Update()
    {
        Vector3[] vertices = Mesh.mesh.vertices;
        
        // Bepaal de nieuwe positie voor de vertices
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        }
        
        // Zet de vertices goed
        Mesh.mesh.vertices = vertices;
        Mesh.mesh.RecalculateNormals();
    }
}
