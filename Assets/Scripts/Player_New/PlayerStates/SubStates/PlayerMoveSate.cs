using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSate : PlayerGroundedState
{
    public PlayerMoveSate(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        // ����ˮƽ�ٶ�
        player.SetVelocityX(playerData.movementVelocity * xInput);

        player.CheckIfShouldFlip(xInput);       //����Ƿ���Ҫ��ת

        // ������ˮƽ����Ϊ0,���л���Idle״̬
        if (xInput == 0 && !isExitingState)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }

        // ת��Ϊ�����ƶ�״̬
        //else if()
        //{

        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
