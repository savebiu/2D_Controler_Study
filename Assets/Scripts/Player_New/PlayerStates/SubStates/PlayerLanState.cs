using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanState : PlayerGroundedState
{
    private bool isGrounded;        //�Ƿ��ڵ�����
    public PlayerLanState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();      //����Ƿ��ڵ�����

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


       
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
