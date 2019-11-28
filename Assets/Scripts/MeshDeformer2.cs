using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer2 : MonoBehaviour
{
    private Mesh deformingMesh; //mesh do deformacji
    private Vector3[] originalVertices, displacedVertices; //oryginalne polozenie vertexow i nowe
    private Vector3[] vertexVelocities; //predkosc vertexow

    private void Start()
    {
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
        
        vertexVelocities = new Vector3[originalVertices.Length];
    }
}
