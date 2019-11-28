using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleMesh : MonoBehaviour
{
    
    private Vector3[] vertices;
    private int[] triangles;

    public int xSize = 10;
    public int ySize = 10;

    private int xVertices;
    private int yVertices;
    
    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = _mesh;

        xVertices = xSize + 1;
        yVertices = ySize + 1;
        
        vertices = new Vector3[xVertices*yVertices];
        triangles = new int[xSize * ySize * 6];
        
        FillVerticesArray();
        UpdateMesh();
        
    }

    private void Update()
    {
        FillVerticesArray();
        UpdateMesh();
    }

    void FillVerticesArray()
    {
        Vector3 tempVert = Vector3.zero;

        for (int y = 0, i = 0; y < yVertices; y++)
        {
            for (int x = 0; x < xVertices; x++)
            {
                tempVert.x = x;
                tempVert.y = y;

                vertices[i] = tempVert;

                i++;
            }
        }
    }
    
    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(vertices[i], 0.0666f);
        }
    }

    IEnumerator MakeMesh()
    {
        yield return new WaitForSeconds(0.15f);
    }


    
}
