using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private float moveDirection = 0f;

    private bool jumpPressed = false;

    private bool interactPressed = false;

    private bool shootPressed = false;


    private static InputManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There are more InputManager in the scene.");
        }
        instance = this;
    }

    public static InputManager GetInstance()
    {
        return instance;
    }

    public void MovePressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            moveDirection = ctx.ReadValue<float>();
        }
        else if (ctx.canceled)
        {
            moveDirection = ctx.ReadValue<float>();
        }
    }

    public void JumpPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            jumpPressed = true;
        }
        else if (ctx.canceled)
        {
            jumpPressed = false;
        }
        
    }

    public void InteractButtonPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            interactPressed = true;

        }
        else if (ctx.canceled)
        {
            interactPressed = false;
        }
    }

    public void ShootPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            shootPressed = true;
        }
        else if (ctx.canceled)
        {
            shootPressed = false;
        }
    }

    public float GetMoveDirection()
    {
        return moveDirection;
    }

    public bool GetJumpPressed()
    {
        bool result = jumpPressed;
        jumpPressed = false;
        return result;
    }

    public bool GetShootPressed()
    {
        bool result = shootPressed;
        shootPressed = false;
        return result;
    }
    public bool GetInteractPressed()
    {
        bool result = interactPressed;
        interactPressed = false;
        return result;
    }

}
