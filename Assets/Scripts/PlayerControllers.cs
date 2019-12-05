using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControllers : MonoBehaviour
{
    //serialized fields
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private bool m_AirControl = true;
    [SerializeField] private LayerMask m_WhatIsGround;
    //[SerializeField] private Transform m_GroundCheck;
    //[SerializeField] private Transform m_CeilingCheck;
    //[SerializeField] private Collision2D m_CrouchDisableCollider;
    //[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = 0.36f;
    [Range(0, 0.3f)] [SerializeField] private float m_MovementSmoothing = 0.5f;

    private const float k_GroundedRadius = 0.2f;
    private Rigidbody2D m_Rigidbody2D;
    private bool m_Grounded;
    private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space] 
    public UnityEvent OnLandEvent;
    
    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        
        if(OnLandEvent==null) OnLandEvent = new UnityEvent();
        
    }

    private void FixedUpdate()
    {
        //bool wasGrounded = m_Grounded;
        //m_Grounded = false;
        
        //Gracz jest uziemiony jeżeli circlecast od ground checku uderzy w cos co jest groundem
        /*Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject!=gameObject)
        }*/
        OnLandEvent.Invoke();
    }

    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        
    }
}
