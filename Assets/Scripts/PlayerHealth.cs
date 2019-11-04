using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHP = 20f;

    private float healthPoints;

    private void Start()
    {
        healthPoints = maxHP;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            healthPoints = 0;
            gameObject.SetActive(false);
        }
    }
}
