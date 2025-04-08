using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected int Xinput;

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

        Xinput = player.InputHandle.NormInputX;      //获取控制器x输入数据

        if (isGrounded)
        {
            player.stateMachine.ChangeState(player.IdleState);
        }

        else if (Xinput == 0 && !isTouchingWall)
        {
            player.stateMachine.ChangeState(player.InAirState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}