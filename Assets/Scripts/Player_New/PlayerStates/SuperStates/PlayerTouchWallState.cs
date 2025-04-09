using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isGrounded;    
    protected bool isTouchingWall;

    protected int xInput;
    protected int yInput;
    protected bool grabInput;        //ץȡ����
    protected bool jumpInput;        //��Ծ����

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

        xInput = player.InputHandle.NormInputX;      //��ȡ������x��������
        yInput = player.InputHandle.NormInputY;      //��ȡ������y��������
        grabInput = player.InputHandle.GrabInput;        //��ȡ������ץȡ�������� 
        jumpInput = player.InputHandle.JumpInput;        //��ȡ��������Ծ��������

        // ǽ����Ծ
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