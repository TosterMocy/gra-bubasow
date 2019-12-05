using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpHeight = 4f;

    private Vector2 horizontalMovement;
    private float playerScaleX;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
        playerScaleX = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = new Vector2(Input.GetAxis("Horizontal"),0);
        
        
        if (horizontalMovement.x < 0) playerScaleX = -1;
        if (horizontalMovement.x > 0) playerScaleX = 1;
        transform.localScale = new Vector3(playerScaleX, 1f, 1f);
        
    }

    void MovePlayer(Vector2 dir)
    {
        controller.Move(dir * Time.deltaTime * movementSpeed);
    }
}
