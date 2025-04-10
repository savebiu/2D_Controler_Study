﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandle : MonoBehaviour
{

    public Vector2 MovementInput { get; private set; }      // 获取原始移动输入
    public bool JumpInput { get; private set; }              // 获取跳跃输入
    public bool JumpInputStop { get; private set; }      // 获取跳跃输入停止时间 -- 实现可控跳跃高度

    public bool GrabInput { get; private set; }              // 获取抓取输入

    // 使用保留输入时间的方式,能在超过这个时间以后不再接收输入信息
    [SerializeField]
    private float inputHoldTime = 0.2f;       // 输入保持时间

    private float jumpInputStartTime;         // 跳跃输入开始时间

    /*
     * 标准化输入的作用是将输入的值转换为-1, 0, 1
     * 防止使用控制器控制时出现速度不一致的情况
     */
    public int NormInputX { get; private set; }      // 标准化X轴输入
    public int NormInputY { get; private set; }      // 标准化Y轴输入


    private void Update()
    {
        CheckJumpInputholdTime();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // 获取context值
        MovementInput = context.ReadValue<Vector2>();
        
        if(Mathf.Abs(MovementInput.x) > 0.5f)
        {
            NormInputX = (int)(MovementInput * Vector2.right).normalized.x;     //对x值进行归一化处理
        }
        else
        {
            NormInputX = 0;       //如果x值小于0.5,则设置为0
        }
        if(Mathf.Abs(MovementInput.y) > 0.5f)
        {
            NormInputY = (int)(MovementInput * Vector2.up).normalized.y;        //对y值进行归一化处理
        }
        else
        {
            NormInputY = 0;       //如果y值小于0.5,则设置为0
        }
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
        // 按下输入键
        if (context.started)
        {
            //CheckJumpInput();       // 锁定跳跃状态
            JumpInput = true;       // 设置跳跃状态为真
            JumpInputStop = false;      // 设置跳跃输入停止为假
            jumpInputStartTime = Time.time;     // 记录跳跃开始时间
        }

        // 松开输入键
        if (context.canceled)
        {
            JumpInputStop = true;       
        }
    }

    // 锁定跳跃,，在进行一次跳跃后转换为false
    // 防止按下按钮立即松开后停止跳跃
    public void CheckJumpInput() => JumpInput = false;

    // 在超过输入时间以后,不再接收跳跃输入
    public void CheckJumpInputholdTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        // 获取context值
        if (context.started)
        {
            GrabInput = true;       // 设置抓取状态为真
        }
        if (context.canceled)
        {
            GrabInput = false;      // 设置抓取状态为假
        }
    }
}
