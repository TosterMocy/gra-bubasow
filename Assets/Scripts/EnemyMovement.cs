using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public float movementSpeed = 10f;
    public float jumpHeight = 200f;
    
    private Rigidbody rb;
    private Transform destination;
    private Vector2 direction;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        
    }

    private void Move()
    {
        direction = new Vector2(-1,0);
        //rb.MovePosition(direction*movementSpeed*Time.deltaTime);
    }

    private void Jump()
    {
        Debug.Log("jump");
        rb.AddForce(new Vector2(0,jumpHeight));
        //rb.velocity = new Vector2(0,jumpHeight);
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Jump();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }
}
