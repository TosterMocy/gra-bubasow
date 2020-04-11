using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeDBlob : MonoBehaviour
{
    public GameObject _prefabPoint;

    private List<Vector3> vertices;
    private GameObject midPoint;
    private MeshFilter meshFilter;
    private MeshRenderer _meshRenderer;
    
    private void Start()
    {
         meshFilter = gameObject.GetComponent<MeshFilter>();
         _meshRenderer = gameObject.GetComponent<MeshRenderer>();
         
        midPoint = Instantiate(_prefabPoint, transform.position,Quaternion.identity);
        midPoint.name = "MidPoint";
        foreach (var point in meshFilter.mesh.vertices)
        {
            var objectPoint =  Instantiate(_prefabPoint, point, Quaternion.identity);
            objectPoint.transform.parent = midPoint.transform;
        }

        vertices = new List<Vector3>();
        foreach (Transform child in midPoint.transform)
        {
            vertices.Add(child.position);
        }
        var _mesh = new  Mesh();
        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = meshFilter.mesh.triangles;
        _meshRenderer.enabled = true;
        meshFilter.mesh = _mesh;
        _meshRenderer.material = Resources.Load<Material>("Materials/blobby");

        foreach (Transform child in midPoint.transform)
        {
           var springJoint = midPoint.AddComponent<SpringJoint>();
           springJoint.connectedBody = child.GetComponent<Rigidbody>();
           springJoint.spring = 10f;
           springJoint.autoConfigureConnectedAnchor = true;

           child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void LateUpdate()
    {
        int i = 0;
        foreach (Transform child in midPoint.transform)
        {
            vertices[i] = child.position;
            i++;
        }
        var _mesh = new  Mesh();
        _mesh.vertices = vertices.ToArray();
        _mesh.triangles = meshFilter.mesh.triangles;

        meshFilter.mesh = _mesh;    }
}
