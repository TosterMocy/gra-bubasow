using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] Waypoints;


    private Rigidbody rb;
    private Transform destination;
    private Vector2 direction;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        destination = 
    }

    private void FindTarget()
    {
        direction = 
    }


}
