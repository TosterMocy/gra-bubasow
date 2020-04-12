using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class BlobGenerator2D : MonoBehaviour
{
    // Start is called before the first frame update
    public int size = 10;
    public float distanceBetweenPoints = 3;
    public float radius = 5.0f;
    public bool isPlayer = false;
    public GameObject blob;
    private GameObject midPoint;
    private List<Transform> pointArray = new List<Transform>();
    private List<Transform> OuterPointArray = new List<Transform>();
    private List<Vector3> vertexPoints;
    private MeshFilter _meshFilter;

    private List<Vector2> _UV;
    
    
    //mesh generation
    private Vector3[] vertices;
    private List<int> triangles;
    
    public Mesh _mesh;
    
    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        if(isPlayer)
            Gizmos.color = Color.yellow;
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(transform.position, radius);
    }
    
    void Start()
    {
        PreparePoints();
        MakeCircle(pointArray,radius);
        SetMidPointJoints();
        SetPointsJoints(pointArray);
        this.gameObject.AddComponent<MeshFilter>();
        this.gameObject.AddComponent<MeshRenderer>();
        this.gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/blobby");
        
        _mesh = new Mesh();

        
        vertexPoints = new List<Vector3>();
        _UV = new List<Vector2>();
        
        vertexPoints.Add(midPoint.transform.localPosition);
        _UV.Add(Vector2.one * 0.5f);
        
        for (int i = 1; i < size + 1; i++)
        {
            vertexPoints.Add( pointArray[i - 1].localPosition);
            _UV.Add(((Vector2)pointArray[i - 1].localPosition.normalized + Vector2.one) *0.5f);
        }

        var tris = 3 * vertexPoints.Count +3;
        triangles = new List<int>();
        int j = 1;
        for (int i = 0; i < tris; i+=3)
        {
            
            triangles.Add(0);
            triangles.Add((j + 1) % (size + 1));
            triangles.Add((j + 2) % (size + 1));
            j++;
            
        }
        triangles.Add(0);
        triangles.Add(size);
        triangles.Add(1);

        midPoint.GetComponent<CircleCollider2D>().radius = 0.01f;
        
        _mesh.Clear();
        _mesh.vertices = vertexPoints.ToArray();
        _mesh.triangles = triangles.ToArray();
        _meshFilter = gameObject.GetComponent<MeshFilter>();
        _mesh.uv = _UV.ToArray();
        _meshFilter.mesh = _mesh;


    }

    
    private void LateUpdate()
    {
        vertexPoints[0] = midPoint.transform.localPosition;
        for (int i = 1; i < size + 1; i++)
        {
            vertexPoints[i] = pointArray[i - 1].localPosition;
            _UV[i] = ( ((Vector2)pointArray[i - 1].position - (Vector2)midPoint.transform.position).normalized + Vector2.one) *0.5f;
        }

        _mesh.Clear();
        _mesh.vertices = vertexPoints.ToArray();
        _mesh.triangles = triangles.ToArray();
        _mesh.uv = _UV.ToArray();
        _meshFilter.mesh = _mesh;
        _mesh.RecalculateNormals();
    }


    private void SetPointsJoints(List<Transform> points)
    {
      
        for (int i = 0; i < size; i++)
        {
            var spring = points[i].GetComponent<SpringJoint2D>();
            spring.connectedBody = points[(i + 1) % (size)].GetComponent<Rigidbody2D>();
           // spring.distance = distanceBetweenPoints;
            spring.frequency = 5f;
            spring.autoConfigureConnectedAnchor = true;
            spring.autoConfigureDistance = true;

        }
    }

    private void SetMidPointJoints()
    {
        var midPointSprings = midPoint.GetComponents<SpringJoint2D>();
               for (int i = 0; i < size; i++)
               {
                   midPointSprings[i].enableCollision = true;
                   midPointSprings[i].autoConfigureDistance = false;
                   midPointSprings[i].distance = radius;
                   midPointSprings[i].dampingRatio = 1;
                   midPointSprings[i].frequency = 1f;
                   midPointSprings[i].connectedBody = pointArray[i].GetComponent<Rigidbody2D>();
               }
    }
    
    private void PreparePoints()
    {
        midPoint = Instantiate(blob, this.transform);
        midPoint.transform.localScale = Vector3.one *2.1f;
        midPoint.GetComponent<Rigidbody2D>().mass = 1f;
        //midPoint.GetComponent<SpriteRenderer>().enabled = true;
            
        midPoint.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        if(isPlayer)
            midPoint.AddComponent<BlobMovement>();
        else
        {
            midPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        
       for (int i = 0; i < size; i++)
       {
           GameObject point = Instantiate(blob, this.transform);
          // if(!isPlayer) point.GetComponent<SpriteRenderer>().color = Color.red;
          
           point.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
           point.GetComponent<Rigidbody2D>().mass = 0.5f;
           point.AddComponent(typeof(SpringJoint2D));
           point.GetComponent<SpringJoint2D>().enableCollision = true;
           point.GetComponent<SpringJoint2D>().autoConfigureDistance = false;
           point.GetComponent<SpringJoint2D>().distance = 1f;
           point.SetActive(true);
           pointArray.Add(point.transform);
           midPoint.AddComponent(typeof(SpringJoint2D));
       }


       
    }
    
    void MakeCircle(List<Transform> points, float rad)
    {
        Vector3 center = transform.position;
        for (int i = 0; i < size; i++)
        {
            int angle = 360 / size * i;
            points[i].position = SetPointOnCircle(center, rad, angle);
        }
        
        Vector3 SetPointOnCircle(Vector3 cirlceMid, float circleRadius, int angle)
        {
            Vector3 pos;
            pos.x = cirlceMid.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
            pos.y = cirlceMid.y + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            pos.z = cirlceMid.z;
            return pos;
        }
    }
    
    
    
    private Vector3 CalculateBezierPoint (float t, Vector3 p0, Vector3 p1)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
    
        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
     
        return p;
    }
    

    
    
}


