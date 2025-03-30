using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{

    public Vector2 MovementInput { get; private set; }      // 获取原始移动输入
    public bool JumpInput { get; private set; }              // 获取跳跃输入

    /*
     * 标准化输入的作用是将输入的值转换为-1, 0, 1
     * 防止使用控制器控制时出现速度不一致的情况
     */
    public int NormInputX { get; private set; }      // 标准化X轴输入
    public int NormInputY { get; private set; }      // 标准化Y轴输入


    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // 获取context值
        MovementInput = context.ReadValue<Vector2>();
         
        NormInputX = (int)(MovementInput * Vector2.right).normalized.x;     //对x值进行归一化处理
        NormInputY = (int)(MovementInput * Vector2.up).normalized.y;        //对y值进行归一化处理
        // Debug.Log(MovementInput);
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        #region 1. 获取context值

        //// context.started: 按下
        //if (context.started)
        //{
        //    Debug.Log("Jump button pushed down now");
        //}

        //// context.performed: 按住
        //if (context.performed)
        //{
        //    Debug.Log("Jump button held now");
        //}

        //// context.canceled: 松开
        //if (context.canceled)
        //{
        //    Debug.Log("Jump button has been released");
        //}

        #endregion  
        // 输入值为真
        if (context.started)
        {
            CheckJumpInput();       // 锁定跳跃状态
            JumpInput = true;
        }

    }

    // 锁定跳跃,防止按下按钮立即松开后停止跳跃
    public void CheckJumpInput() => JumpInput = false;
}
