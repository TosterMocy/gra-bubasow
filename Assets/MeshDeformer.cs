using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[RequireComponent(typeof(MeshFilter))]
public class MeshDeformer : MonoBehaviour
{
    private Mesh deformingMesh;
    Vector3[]  originalVertices, displacedVertices;
    Vector3[] vertexVelocities;
    private Rigidbody rb;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        deformingMesh = this.GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];

        for (int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }
        
        vertexVelocities = new Vector3[originalVertices.Length];
    }
    float uniformScale = 1f;
    void Update () {
        uniformScale = transform.localScale.x;
        for (int i = 0; i < displacedVertices.Length; i++) {
            UpdateVertex(i);
        }
        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();
    }
    
    public void AddDeformingForce (Vector3 point, float force) {
        point = transform.InverseTransformPoint(point);
        for (int i = 0; i < displacedVertices.Length; i++) 
        {
            AddForceToVertex(i, point, force);
        }
    }
    void AddForceToVertex (int i, Vector3 point, float force)
    {
        Vector3 pointToVertex = displacedVertices[i] - point;
        pointToVertex *= uniformScale;
        float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
        float velocity = attenuatedForce * Time.deltaTime;
        vertexVelocities[i] += pointToVertex.normalized * velocity;
    }
    
    
    public float springForce = 30f;
    public float damping = 20f;
    void UpdateVertex (int i) {
        Vector3 velocity = vertexVelocities[i];
        Vector3 displacement = displacedVertices[i] - originalVertices[i];
        displacement *= uniformScale;
        velocity -= displacement * springForce * Time.deltaTime;
        velocity *= 1f - damping * Time.deltaTime;
        vertexVelocities[i] = velocity;
        displacedVertices[i] += velocity  * (Time.deltaTime / uniformScale);
    }

    private void OnCollisionEnter(Collision other)
    {
        var contact = other.GetContact(0);
        Debug.Log(originalVertices.Length);
        //Debug.Log(rb.velocity.magnitude * 1000f);
        this.AddDeformingForce(contact.point, Random.Range(100,250));
    }
}