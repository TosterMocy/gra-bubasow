using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem splashParticles;

    private void Start()
    {
        //splashParticles.transform.position = transform.position;
    }
    
    


    public void ChildCollisionDetection(Transform blobPoint)
    {
        Debug.Log("pozycja particli 1: " + splashParticles.transform.position);
        Debug.Log("pozycja blobow w rodzicu " + blobPoint.position);
        
        splashParticles.transform.position = blobPoint.position;
        
        Debug.Log("nowa pozycja particli: " + splashParticles.transform.position);
        splashParticles.Play();
        
        //Debug.Log("kolizja rodzica");
    }
}
