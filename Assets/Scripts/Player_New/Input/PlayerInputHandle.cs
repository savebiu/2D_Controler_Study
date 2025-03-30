using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{

    public Vector2 MovementInput { get; private set; }      // ��ȡ�ƶ�����


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // ��ȡcontextֵ
        MovementInput = context.ReadValue<Vector2>();
        // Debug.Log(MovementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        #region 1. ��ȡcontextֵ

        //// context.started: ����
        //if (context.started)
        //{
        //    Debug.Log("Jump button pushed down now");
        //}

        //// context.performed: ��ס
        //if (context.performed)
        //{
        //    Debug.Log("Jump button held now");
        //}

        //// context.canceled: �ɿ�
        //if (context.canceled)
        //{
        //    Debug.Log("Jump button has been released");
        //}

        #endregion  

    }
}
