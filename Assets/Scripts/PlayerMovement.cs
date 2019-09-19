using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 1f;

    private Vector2 directionOfMovement;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) directionOfMovement = new Vector2(-1, 0);
        if (Input.GetKeyDown(KeyCode.D)) directionOfMovement = new Vector2(1, 0);
        if (!Input.anyKeyDown)directionOfMovement = Vector2.Lerp(directionOfMovement,Vector2.zero, 0.1f);
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
