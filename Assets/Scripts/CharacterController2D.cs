using Assets.Scripts.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{

    [Header("Movement Params")]
    public float moveAccelearation = 6.0f;
    public float maxVelocity = 10f;
    public float jumpForce = 8.0f;
    public float gravityScale = 20.0f;


    private BoxCollider2D coll;
    private Rigidbody2D rb;

    private IGroundDetection groundDetection;
    private IAttackMethod attackMethod;
    

    private void Awake()
    {
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        groundDetection = GetComponentInChildren<IGroundDetection>();
        attackMethod = GetComponent<IAttackMethod>();



        rb.gravityScale = gravityScale;

        
    }

    private void FixedUpdate()
    {
        //No movement from the player while dialogue is active
        if (DialogueManager.GetInstance().DialogueIsPlaying)
        {
            return;
        }

        HandleMovement();

        HandleJump();

        HandleAttack();
    }

    private void HandleAttack()
    {
        bool isAttacking = InputManager.GetInstance().GetShootPressed();

        if (isAttacking)
        {
            attackMethod.Attack();
        }
    }

    private void HandleMovement()
    {
        float moveDirection = InputManager.GetInstance().GetMoveDirection();

        rb.AddForce(moveDirection * transform.right * moveAccelearation);

        rb.velocity = ClampOnX(rb.velocity, maxVelocity);
    }

    private void HandleJump()
    {
        bool jumpPressed = InputManager.GetInstance().GetJumpPressed();

        if (groundDetection.OnGround && jumpPressed)
        {
            
            rb.AddForce(transform.up * jumpForce);
        }
    }

    private Vector2 ClampOnX(Vector2 vector, float maxX)
    {
        return new Vector2(Math.Clamp(vector.x, -maxX, maxX), vector.y);
    }
}
