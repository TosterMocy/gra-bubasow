using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobGenerator2D : MonoBehaviour
{
    // Start is called before the first frame update
    public int size = 10;
    public GameObject blob;
    
    private List<GameObject> pointArray = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject point = Instantiate(blob,this.transform);
            pointArray.Add(point);
            point.SetActive(false);
            point.AddComponent(typeof(SpringJoint2D));
            point.GetComponent<SpringJoint2D>().enableCollision = true;
            point.GetComponent<SpringJoint2D>().autoConfigureDistance = false;
            point.GetComponent<SpringJoint2D>().distance = 1;
            point.SetActive(true);
            
            this.gameObject.AddComponent(typeof(SpringJoint2D));
            
           // this.GetComponent<SpringJoint2D>().connectedBody = point.GetComponent<Rigidbody2D>();
        }
        
        
        Vector3 center = transform.position;
        for (int i = 0; i < size; i++)
        {
            int a = 360 / size * i;
            pointArray[i].transform.position = RandomCircle(center, 1.0f ,a);
          
        }
        
        
        

        var springs = this.gameObject.GetComponents<SpringJoint2D>();
        for (int i = 0; i < size; i++)
        {
            springs[i].enableCollision = true;
            springs[i].autoConfigureDistance = false;
            springs[i].distance = 1;
            springs[i].connectedBody = pointArray[i].GetComponent<Rigidbody2D>();
     
        }
        
        for (int i = 0; i < size; i++)
        {
            pointArray[i].GetComponent<SpringJoint2D>().connectedBody = pointArray[(i+1)%(size)].GetComponent<Rigidbody2D>();
        }

    }
    Vector3 RandomCircle(Vector3 center, float radius,int a)
    {
        Debug.Log(a);
        float ang = a;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
