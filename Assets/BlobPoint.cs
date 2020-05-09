using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobPoint : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (transform.parent.GetComponent<Player>())
        {
            if (other.transform.CompareTag("Ground"))
            {
                transform.parent.GetComponent<Player>().ChildCollisionDetection(transform);
                
                Debug.Log("BlobPoint pozycja" + transform.position);
                //Debug.Log("kolizja dziecka");
            }
        }
    }
}
