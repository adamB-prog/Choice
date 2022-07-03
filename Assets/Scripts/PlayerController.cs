
using Assets.Scripts;
using Assets.Scripts.Interfaces;
using Choice.Input;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private PlayerControls playerControls;
    
    private Rigidbody2D rb;


    private IGroundDetection groundDetector;

    private bool moving = false;

    
    private bool jumping = false;

    
    

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float jumpForce = 1f;

    [SerializeField]
    private float maxVelocity = 1f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundDetector = gameObject.GetComponentInChildren<IGroundDetection>();
        

        playerControls.Land.Jump.performed += Jump;
        playerControls.Land.Jump.canceled += Jump_Canceled;
        playerControls.Land.Move.performed += Move;
        playerControls.Land.Move.canceled += Move_Canceled;
    }

    private void Jump_Canceled(InputAction.CallbackContext context)
    {
        jumping = false;
    }

    private void Move_Canceled(InputAction.CallbackContext context)
    {
        moving = false;
    }

    private void Move(InputAction.CallbackContext context)
    {
        //Debug.Log(context.ReadValue<float>());
        moving = true;
        
    }


    private void FixedUpdate()
    {
        if (moving)
        {
            float movementX = playerControls.Land.Move.ReadValue<float>();
            rb.AddForce(movementX * transform.right * speed);
        }
        
        if (jumping && groundDetector.OnGround)
        {
            rb.AddForce(transform.up * jumpForce);

           
        }

        rb.velocity = ClampOnX(rb.velocity, maxVelocity);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        //Debug.Log("Jump");
        

        jumping = true;
    }

    


    private Vector2 ClampOnX(Vector2 vector, float maxX)
    {
        return new Vector2(Math.Clamp(vector.x, -maxX, maxX), vector.y);
    }
}
