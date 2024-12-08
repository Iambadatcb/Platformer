using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 10;
    public float jumpHeight = 3;

    [Header("Jump")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;

    [Header("Jump Mechanics")]
    public float coyoteTime = 0.3f;
    
    private float coyoteTimeCounter;
    private bool isGrounded;
    private Rigidbody2D rb;
    private float inputX;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundCheckRadius,groundLayer);

        if(isGrounded){
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter-= Time.deltaTime;
        }

        if(coyoteTimeCounter>0 && Input.GetButtonDown("Jump"))
        {
            var jumpVelocity = MathF.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpVelocity);
        }
        if(coyoteTimeCounter<0 && Input.GetButtonDown("Jump") && !isGrounded){
            var jumpVelo = MathF.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);
            rb.velocity = new UnityEngine.Vector2(rb.velocity.x, jumpVelo);
        }
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new UnityEngine.Vector2(inputX * movementSpeed, rb.velocity.y);
    }
}
