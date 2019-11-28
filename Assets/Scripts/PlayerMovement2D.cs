using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpHeight = 4f;

    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Input.GetAxis("Horizontal")
    }
}
