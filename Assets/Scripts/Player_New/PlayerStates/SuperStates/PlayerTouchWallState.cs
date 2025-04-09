using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isGrounded;    
    protected bool isTouchingWall;

    protected int xInput;
    protected int yInput;
    protected bool grabInput;        //抓取输入
    protected bool jumpInput;        //跳跃输入

    public PlayerTouchWallState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isTouchingWall = player.CheckIfTouchingWall();
        isGrounded = player.CheckIfGrounded();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.InputHandle.NormInputX;      //获取控制器x输入数据
        yInput = player.InputHandle.NormInputY;      //获取控制器y输入数据
        grabInput = player.InputHandle.GrabInput;        //获取控制器抓取输入数据 
        jumpInput = player.InputHandle.JumpInput;        //获取控制器跳跃输入数据

        // 墙壁跳跃
        if (jumpInput)
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            player.stateMachine.ChangeState(player.WallJumpState);
        }

        else if (isGrounded && !grabInput)
        {
            player.stateMachine.ChangeState(player.IdleState);
        }

        else if (xInput == 0 && !isTouchingWall)
        {
            player.stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}