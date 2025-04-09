using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int Yinput;

    public bool JumpInput;
    public bool GrabInput;        //抓取输入
    public bool isGrounded;        //是否在地面上
    public bool isTouchingWall;      //是否碰到墙壁

    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountofJump();        //重置跳跃次数
        isGrounded = player.CheckIfGrounded();      //检测是否在地面上
        isTouchingWall = player.CheckIfTouchingWall();     //检测是否碰到墙壁
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandle.NormInputX;      //获取x输入数据
        Yinput = player.InputHandle.NormInputY;      //获取y输入数据
        JumpInput = player.InputHandle.JumpInput;        //获取跳跃输入数据
        GrabInput = player.InputHandle.GrabInput;        //获取抓取输入数据

        // 跳跃状态为真 && 能够跳跃,则切换到跳跃状态
        if (JumpInput && player.JumpState.CanJump())
        {
            //Debug.Log("entro Jump");
            player.InputHandle.CheckJumpInput();        // 检查跳跃输入，防止一直跳跃
            playerStateMachine.ChangeState(player.JumpState);
        }

        // 不在地面上,则切换到空中状态
        else if (!isGrounded)
        {
            player.InAirState.StartCoyoteTime();      //启动土狼时间
            player.stateMachine.ChangeState(player.InAirState);
        }

        // 在墙上并且按下抓取键,则切换到墙壁抓取状态        
        else if (isTouchingWall && GrabInput)
        {
            player.stateMachine.ChangeState(player.WallGrabState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
