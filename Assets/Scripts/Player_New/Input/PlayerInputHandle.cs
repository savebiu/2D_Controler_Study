using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{

    public Vector2 MovementInput { get; private set; }      // ��ȡԭʼ�ƶ�����
    public bool JumpInput { get; private set; }              // ��ȡ��Ծ����

    /*
     * ��׼������������ǽ������ֵת��Ϊ-1, 0, 1
     * ��ֹʹ�ÿ���������ʱ�����ٶȲ�һ�µ����
     */
    public int NormInputX { get; private set; }      // ��׼��X������
    public int NormInputY { get; private set; }      // ��׼��Y������


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // ��ȡcontextֵ
        MovementInput = context.ReadValue<Vector2>();
         
        NormInputX = (int)(MovementInput * Vector2.right).normalized.x;     //��xֵ���й�һ������
        NormInputY = (int)(MovementInput * Vector2.up).normalized.y;        //��yֵ���й�һ������
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
        // ����ֵΪ��
        if (context.started)
        {
            CheckJumpInput();       // ������Ծ״̬
            JumpInput = true;
        }

    }

    // ������Ծ,��ֹ���°�ť�����ɿ���ֹͣ��Ծ
    public void CheckJumpInput() => JumpInput = false;
}
