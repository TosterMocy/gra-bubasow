using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public float jumpSpeed = 20f;

    private Vector2 directionOfMovement;
    private float horizontalMovement;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        directionOfMovement = new Vector2(horizontalMovement,0);
        
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void FixedUpdate()
    {
        MoveCharacter(directionOfMovement);
        
    }

    void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (Time.deltaTime * movementSpeed * dir));
        
    }

    void Jump()
    {
        Debug.Log("skacze");
        rb.AddForce(transform.up * jumpSpeed);
    }
}
