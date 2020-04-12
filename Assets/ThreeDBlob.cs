using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ThreeDBlob : MonoBehaviour
{
    public GameObject _prefabPoint;
    private List<Vector3> vertices;
    private List<Transform> midPointChildren;
    private GameObject midPoint;
    private MeshFilter meshFilter;
    private MeshRenderer _meshRenderer;
    private Mesh _mesh;
    private List<Vector2> _uv;
    
    private void Awake()
    {
         meshFilter = gameObject.GetComponent<MeshFilter>();
         _meshRenderer = gameObject.GetComponent<MeshRenderer>();
         
        midPoint = Instantiate(_prefabPoint, transform.position,Quaternion.identity);

        midPoint.transform.parent = transform;
        midPoint.name = "MidPoint";
        foreach (var point in meshFilter.mesh.vertices)
        {
            var objectPoint =  Instantiate(_prefabPoint, point, Quaternion.identity);
            objectPoint.transform.parent = midPoint.transform;
        }

        vertices = new List<Vector3>();
        _uv = new List<Vector2>();
        
        foreach (Transform child in midPoint.transform)
        {
            vertices.Add(child.position);
            _uv.Add(child.localPosition.normalized);
        }
        _mesh = new  Mesh();
        _mesh.vertices = vertices.ToArray();
        _mesh.uv = _uv.ToArray();

        _mesh.triangles = meshFilter.mesh.triangles;


        _meshRenderer.enabled = true;
        meshFilter.mesh = _mesh;
        _meshRenderer.material = Resources.Load<Material>("Materials/blobby3D");
        _meshRenderer.shadowCastingMode = ShadowCastingMode.On;

        midPointChildren = new List<Transform>();
        foreach (Transform child in midPoint.transform)
        {
           var springJoint = midPoint.AddComponent<SpringJoint>();
           springJoint.connectedBody = child.GetComponent<Rigidbody>();
           springJoint.spring = 10f;
           springJoint.autoConfigureConnectedAnchor = true;
           child.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
           midPointChildren.Add(child);
        }
        midPoint.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void LateUpdate()
    {
        for(int i =0; i<midPointChildren.Count;i++)
        {
            vertices[i] = midPointChildren[i].position;
            _uv[i] = midPointChildren[i].localPosition.normalized;
            
        }

        _mesh.vertices = vertices.ToArray();
        _mesh.uv = _uv.ToArray();
        _mesh.triangles = meshFilter.mesh.triangles;
        meshFilter.mesh = _mesh;
        meshFilter.mesh.RecalculateNormals();

    }
}
