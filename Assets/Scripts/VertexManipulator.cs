using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class VertexManipulator : MonoBehaviour
{
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3[] normals;

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
    }

    private void Update()
    {
        vertices = mesh.vertices;
        normals = mesh.normals;
        
        for (var i = 0; i < vertices.Length; i++)
        {
            vertices[i] += Vector3.up * Time.deltaTime;
        }

        mesh.vertices = vertices;
    }
}
