using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputManager : MonoBehaviour
{
    private float moveDirection = 0f;

    private Vector2 navigationDirection = Vector2.zero;

    private bool jumpPressed = false;

    private bool interactPressed = false;

    private bool shootPressed = false;

    private bool submitPressed = false;

    private static InputManager instance;

    private void Awake()
    {
        Application.targetFrameRate = -1;
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

    public void NavigationPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            navigationDirection = ctx.ReadValue<Vector2>();
        }
        else if (ctx.canceled)
        {
            navigationDirection = ctx.ReadValue<Vector2>();
        }
    }

    public void SubmitPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            submitPressed = true;
        }
        else if (ctx.canceled)
        {
            submitPressed = false;
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
    
    public Vector2 GetNavigationPressed()
    {
        return navigationDirection;
    }

    public bool GetSubmitPressed()
    {
        bool result = submitPressed;
        submitPressed = false;
        return result;
    }
}
