using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float movementSpeed = 1f;

    private Vector2 directionOfMovement;

    private Rigidbody rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) directionOfMovement = new Vector3(0, 10, 0);
        directionOfMovement = Vector3.Lerp(directionOfMovement,Vector3.zero, 0.1f);

    }

    private void FixedUpdate()
    {
        MoveCharacter(directionOfMovement);
    }

    void MoveCharacter(Vector2 vec)
    {
        rb.MovePosition((Vector2)transform.position + (vec * movementSpeed * Time.deltaTime));
    }
}