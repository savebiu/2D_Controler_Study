using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLanState : PlayerGroundedState
{
    private bool isGrounded;        //是否在地面上
    public PlayerLanState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();      //检测是否在地面上

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
