using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{

    private Vector2 movementInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // 获取context值
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // context.started: 按下
        if (context.started)
        {
            Debug.Log("Jump button pushed down now");
        }

        // context.performed: 按住
        if (context.performed)
        {
            Debug.Log("Jump button held now");
        }

        // context.canceled: 松开
        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }

        // 
    }
}
