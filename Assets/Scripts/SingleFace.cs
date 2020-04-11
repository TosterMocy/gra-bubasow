using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SingleFace : MonoBehaviour
{
    
    public Vector3[] vertices;
    public int[] triangles;


    [Range(0, 2)]  //it tells the editor to make a slider with a range of 0-2
    public int rotate = 0;

    private Mesh _mesh;

    private void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().sharedMesh = _mesh;
        
        CreateFace();
        UpdateMesh();
    }
    

    public void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }

    public void CreateFace()
    {
        
        //vertices
        vertices = new Vector3[4];
        
        vertices[0] = new Vector3(0,0,0); //bottomleft 
        vertices[1] = new Vector3(0,1,0); //top left 
        vertices[2] = new Vector3(1,0,0); //bottom right 
        vertices[3] = new Vector3(1,3,0); //top  right 

        
        //triangles
        triangles = new int[6];

        triangles[0] = 0; //Bottom Left
        triangles[1] = 1; //Top Left
        triangles[2] = 2; //Bottom Right
        
        triangles[3] = 1; //Bottom Left
        triangles[4] = 3; //Top Left
        triangles[5] = 2; //Bottom Right

    }
}
