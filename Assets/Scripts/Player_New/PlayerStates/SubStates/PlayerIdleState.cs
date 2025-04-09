using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName )
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 如果玩家按下了移动键,则切换到移动状态
        if (xInput != 0 && !isExitingState)
        {
            playerStateMachine.ChangeState(player.MoveState);
        }

        // 如果玩家按下了跳跃键,则切换到跳跃状态
        //else if (player.InputHandle.JumpInput)
        //{
        //    playerStateMachine.ChangeState(player.JumpState);
        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
