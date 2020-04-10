using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Transform[] vertexPoints;
    
    
    //mesh generation
    private Vector3[] vertices;
    private int[] triangles;
    
    public Mesh _mesh;
    //private MeshCollider _meshCollider;
    
    
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
        vertexPoints=new Transform[size+1];

        PreparePoints(distanceBetweenPoints);
      //  PrepareOuterPoints(distanceBetweenPoints+2f);
        
        MakeCircle(pointArray,radius);
        
                 //MakeSquare(pointArray,radius); IN PROGRESS
       // MakeCircle(OuterPointArray,radius+3f);
        
        SetMidPointJoints();
      //  SetOuterPointJoints();
        
        SetPointsJoints(pointArray);
       // SetPointsJoints(OuterPointArray);

        this.gameObject.AddComponent<MeshFilter>();
       //mesh generation
       _mesh = new Mesh();
       GetComponent<MeshFilter>().sharedMesh = _mesh;
       
       
       CreateVertices();
       CreateFace();
       
       
       UpdateMesh();

    }

    private void Update()
    {
        UpdateMesh();
    }

    private void SetPointsJoints(List<Transform> points)
    {
      
        for (int i = 0; i < size; i++)
        {
            var spring = points[i].GetComponent<SpringJoint2D>();
            spring.connectedBody = points[(i + 1) % (size)].GetComponent<Rigidbody2D>();
            spring.distance = distanceBetweenPoints;
            spring.frequency = 5f;
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
    
    private void SetOuterPointJoints()
    {
      
        for (int i = 0; i < size; i++)
        {
            var midPointSprings = OuterPointArray[i].GetComponents<SpringJoint2D>();

            for (int j = 0; j < 3; j++)
            {
                midPointSprings[j].enableCollision = true;
                midPointSprings[j].autoConfigureDistance = false;
                
            }
            midPointSprings[0].distance = 4;
            midPointSprings[1].distance = 1;
            midPointSprings[2].distance = 1.5f;
            
            midPointSprings[0].frequency = 2;
            midPointSprings[1].frequency = 4;
            midPointSprings[2].frequency = 5;
            
            midPointSprings[0].connectedBody = OuterPointArray[(i+1)%size].GetComponent<Rigidbody2D>(); //next to him
            midPointSprings[1].connectedBody = pointArray[i].GetComponent<Rigidbody2D>(); //inner circle connection
            midPointSprings[2].connectedBody = pointArray[(i+1)%size].GetComponent<Rigidbody2D>(); // next in innter circle
          
        }
    }

    private void PreparePoints(float rad)
    {
        midPoint = Instantiate(blob, this.transform);
        midPoint.GetComponent<SpriteRenderer>().color = Color.blue;
        midPoint.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        //pointArray.Add(midPoint.transform); 
        
        if(isPlayer)
            midPoint.AddComponent<BlobMovement>();
        else
        {
            midPoint.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        //midPoint.AddComponent<CharacterController>();

        vertexPoints[0] = midPoint.transform;
        Debug.Log(vertexPoints[0]);
        
       for (int i = 0; i < size; i++)
       {
           GameObject point = Instantiate(blob, this.transform);
       
           if(!isPlayer) point.GetComponent<SpriteRenderer>().color = Color.red;
          
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

       for (int i = 0; i < pointArray.Count; i++)
       {
           vertexPoints[i] = pointArray[i];
       }
       
    }
    
    private void PrepareOuterPoints(float rad)
    {
        
        
        for (int i = 0; i < size; i++)
        {
            GameObject point = Instantiate(blob, midPoint.transform);
       
          
            point.SetActive(false);
            
            point.AddComponent(typeof(SpringJoint2D));
            point.AddComponent(typeof(SpringJoint2D));
            point.AddComponent(typeof(SpringJoint2D));
            
            point.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
       
            point.GetComponent<SpringJoint2D>().enableCollision = true;
            point.GetComponent<SpringJoint2D>().autoConfigureDistance = false;
            point.GetComponent<SpringJoint2D>().distance = rad;
            point.SetActive(true);
            OuterPointArray.Add(point.transform);
            
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
    }
    
    
    

    Vector3 SetPointOnCircle(Vector3 center, float circleRadius, int angle)
    {
        Vector3 pos;
        pos.x = center.x + circleRadius * Mathf.Sin(angle * Mathf.Deg2Rad);
        pos.y = center.y + circleRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    
    
    void MakeSquare(List<Transform> points, float rad)
    {
        Vector3 center = transform.position;
        for (int i = 0; i < size; i++)
        {
            int angle = 360 / size * i;
            points[i].position = SetPointOnSquare(center, rad, angle);
        }
    }
    
    Vector3 SetPointOnSquare(Vector3 center, float circleRadius, int angle)
    {
        Vector3 pos;
        float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
        float sin = Mathf.Sin(angle * Mathf.Deg2Rad);

        pos.x = center.x + circleRadius * (cos + cos) *2;
        pos.y = center.y + circleRadius * (cos + sin);
        pos.z = center.z;
        return pos;
    }

    

    void CreateVertices()
    {
        
        vertices = new Vector3[size+1];
        for (int i = 0; i < size; i++)
        {
            vertices[i+1] = vertexPoints[i].position;
            Debug.Log(vertices[i]);
        }

        vertices[0] = midPoint.transform.position;
    }
    void CreateFace()
    {
        

        triangles = new int[(size)*3];
        
        for (int i = 0; i < size; i++)
        {
            triangles[3 * i] = 0;
            triangles[3 * i + 1] = (i)%size + 1;
            triangles[3 * i + 2] = (i+1)%size + 1;
        }
        
        /*
        triangles[size] = j + 1;
        triangles[size - 1] = j;
        triangles[size - 2] = 0;*/

        for (int i = 0; i < triangles.Length; i++)
        {
            Debug.Log(triangles[i]);
        }
        
        Debug.Log(triangles.Length);
       
    }
    
    public void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();

        //_meshCollider.sharedMesh = _mesh;
    }
}


