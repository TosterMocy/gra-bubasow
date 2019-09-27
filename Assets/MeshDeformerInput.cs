﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MeshDeformerInput : MonoBehaviour
{

    public float force = 10f;
    public float forceOffset = 0.1f;
 

    private MeshDeformer deformer;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleInput();
        }
    }
    
    private void HandleInput()
    {
         Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         
         if (Physics.Raycast(inputRay, out hit)) {
              deformer = hit.collider.GetComponent<MeshDeformer>();
              
              if (deformer!=null) {
                  Vector3 point = hit.point;
                  point += hit.normal * forceOffset;
                  deformer.AddDeformingForce(point, force);
              }
         }
         
       
    }
}