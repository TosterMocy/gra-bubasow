using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobGenerator2D : MonoBehaviour
{
    // Start is called before the first frame update
    public int size = 10;
    public float distanceBetweenPoints = 10f;
    public float radius = 3.0f;
    public GameObject blob;
    private GameObject midPoint;
    private List<Transform> pointArray = new List<Transform>();
    private List<Transform> OuterPointArray = new List<Transform>();

    void Start()
    {
        

        PreparePoints(distanceBetweenPoints);
      //  PrepareOuterPoints(distanceBetweenPoints+2f);
        
        MakeCircle(pointArray,radius);
       // MakeCircle(OuterPointArray,radius+3f);
        
        SetMidPointJoints();
      //  SetOuterPointJoints();
        
        SetPointsJoints(pointArray);
       // SetPointsJoints(OuterPointArray);



    }

    private void SetPointsJoints(List<Transform> points)
    {
      
        for (int i = 0; i < size; i++)
        {
            var spring = points[i].GetComponent<SpringJoint2D>();
            spring.connectedBody = points[(i + 1) % (size)].GetComponent<Rigidbody2D>();
            spring.distance = 3f;
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
                   midPointSprings[i].distance = 5f;
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
        midPoint.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        
       for (int i = 0; i < size; i++)
       {
           GameObject point = Instantiate(blob, midPoint.transform);
       
          
           point.SetActive(false);
          
           point.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
           point.GetComponent<Rigidbody2D>().mass = 1f;
       
           point.AddComponent(typeof(SpringJoint2D));
           point.GetComponent<SpringJoint2D>().enableCollision = true;
           point.GetComponent<SpringJoint2D>().autoConfigureDistance = false;
           point.GetComponent<SpringJoint2D>().distance = 1f;
           point.SetActive(true);
           pointArray.Add(point.transform);
       
           midPoint.AddComponent(typeof(SpringJoint2D));
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
            point.GetComponent<SpriteRenderer>().color = Color.red;
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
    
    
    
}


