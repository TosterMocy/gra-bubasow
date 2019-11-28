using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlayer : MonoBehaviour
{
    public GameObject player;
    public float bounceHeight = 2f;
    public BoxCollider bounceCollider;
    private Rigidbody playerRb;

    private void Start()
    {
        playerRb = player.GetComponent<Rigidbody>();
    }

    void Bounce()
    {
        playerRb.AddForce(Vector3.up*bounceHeight, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            Bounce();
        }
    }
}
