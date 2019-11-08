using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleFace : MonoBehaviour
{
    
    public Vector3[] vertices;
    public int[] triangles;

    [Range(0, 2)] 
    public int rotate = 0;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = _mesh;
        
        //vertices
        vertices = new Vector3[3];
        
        vertices[0] = new Vector3(0,0,0); //botton left vertex
        vertices[1] = new Vector3(0,1,0); //top left vertex
        vertices[2] = new Vector3(1,0,0); //bottom right vertex
        
        //triangles
        triangles = new int[3];

        triangles[0] = 0; //BL
        triangles[1] = 1; //TL
        triangles[2] = 2; //BR

        UpdateMesh();
        
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }
}
