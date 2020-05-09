using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BlobMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform pos;
    
    public float force = 150f;
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        pos = transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up *force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right*force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector2.left*force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector2.down*force);
        }
    }

    private void Update()
    {
        Camera.main.transform.position = new Vector3(pos.position.x,pos.position.y,-15);
    }
}
