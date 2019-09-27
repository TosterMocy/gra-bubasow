using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float jumpSpeed = 2f;

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
    }

    private void FixedUpdate()
    {
        MoveCharacter(directionOfMovement);

        if (Input.GetKey(KeyCode.Space)) Jump();
    }

    void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (Time.deltaTime * movementSpeed * dir));
    }

    void Jump()
    {
        //rb.AddForce(Vector2.up * jumpSpeed);
    }
}
