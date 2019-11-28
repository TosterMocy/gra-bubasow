using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuadGeneration : MonoBehaviour
{
    
    public Vector3[] vertices;
    public int[] triangles;

   

    public Mesh _mesh;
    private MeshCollider _meshCollider;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = _mesh;

        _meshCollider = GetComponent<MeshCollider>();
        
        CreateFace();
        UpdateMesh();
        _meshCollider.sharedMesh = _mesh;
    }

    private void Update()
    {
        UpdateMesh();
    }

    public void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();

        //_meshCollider.sharedMesh = _mesh;
    }

    public void CreateFace()
    {
        
        //vertices
        vertices = new Vector3[4];
        
        vertices[0] = new Vector3(0,0,0); //botton left vertex
        vertices[1] = new Vector3(0,1,0); //top left vertex
        vertices[2] = new Vector3(1,0,0); //bottom right vertex
        vertices[3] = new Vector3(1,1,0); //top right vertex
        
        //triangles
        triangles = new int[6];

        triangles[0] = 0; //BL
        triangles[1] = 1; //TL
        triangles[2] = 2; //BR
        
        triangles[3] = 1; //TL
        triangles[4] = 3; //TR
        triangles[5] = 2; //BR
        
        
    }
}
