﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpHeight = 4f;

    private Vector2 directionOfMovement;
    private float horizontalMovement;
    private float verticalMovement;
    private bool isGrounded = false;
    private float playerScaleX;

    private Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        playerScaleX = transform.localScale.x;
    }

    private void Update()
    {
        
        
        //jumping
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
        
        
        horizontalMovement = Input.GetAxis("Horizontal");
        
        directionOfMovement = new Vector2(horizontalMovement,verticalMovement) ;
    }

    private void FixedUpdate()
    {
        MoveCharacter(directionOfMovement);
    }

    void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (Time.deltaTime * movementSpeed * dir));
        if (dir.x < 0) playerScaleX = -1;
        if (dir.x > 0) playerScaleX = 1;

        transform.localScale = new Vector3(playerScaleX, 1f, 1f);

    }

    void Jump()
    {
        //rb.AddForce(Vector2.up * jumpHeight, ForceMode.Impulse);
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        isGrounded = false;

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            //Debug.Log("collision with ground");
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        //Debug.Log("is floating true");
        isGrounded = false;
    }
}
