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

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            //gameObject.SetActive(false);
            healthPoints = 0;
        
        }
    }
}
