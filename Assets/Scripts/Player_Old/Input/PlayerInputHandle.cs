using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{

    private Vector2 movementInput;

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // ��ȡcontextֵ
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        // context.started: ����
        if (context.started)
        {
            Debug.Log("Jump button pushed down now");
        }

        // context.performed: ��ס
        if (context.performed)
        {
            Debug.Log("Jump button held now");
        }

        // context.canceled: �ɿ�
        if (context.canceled)
        {
            Debug.Log("Jump button has been released");
        }

        // 
    }
}
