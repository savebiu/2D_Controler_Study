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

        // �����Ұ������ƶ���,���л����ƶ�״̬
        if (xInput != 0 && !isExitingState)
        {
            playerStateMachine.ChangeState(player.MoveState);
        }

        // �����Ұ�������Ծ��,���л�����Ծ״̬
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
