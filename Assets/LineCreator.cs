using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{

    public GameObject prefab;
    public int size = 5;
    private List<GameObject> chainArray = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject chain = Instantiate(prefab,this.transform);
            chainArray.Add(chain);
            chain.SetActive(false);
        }
        SetPos();
        SetObjects();
        SetActive();
    }

    void SetPos()
    {
        for (int i = 0; i < size; i++)
        {
            if (i == 0)
            {
                chainArray[i].transform.position = new Vector3(0,0,0);
            }
            else
            {
                chainArray[i].transform.position = chainArray[i-1].transform.position - new Vector3(0,-i,0);
            }
           

            
            var joint = chainArray[i].GetComponent<SpringJoint>();
            joint.damper = 10;
            joint.spring = 1;
            joint.maxDistance= 1;
            joint.anchor = Vector3.one;
        }
    }
    
    void SetObjects()
    {
        for (int i = 0; i < size-1; i++)
        {
          
            chainArray[i].GetComponent<SpringJoint>().connectedBody = chainArray[i+1].GetComponent<Rigidbody>();
        }
    }
    void SetActive()
    {
        for (int i = 0; i < size; i++)
        {
            chainArray[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
