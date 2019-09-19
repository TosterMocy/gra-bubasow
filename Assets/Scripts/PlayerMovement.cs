using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 20f;

    private Vector2 directionOfMovement;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            directionOfMovement = Vector2.left;
            rb.AddForce(directionOfMovement*movementSpeed);
        }
        
        if(Input.GetKeyDown(KeyCode.D))
        {
            directionOfMovement = Vector2.right;
            rb.AddForce(directionOfMovement*movementSpeed);
        }
    }
}
