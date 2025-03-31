using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int Xinput;
    protected int Yinput;

    public bool JumpInput;
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
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Xinput = player.InputHandle.NormInputX;      //获取x输入数据
        Yinput = player.InputHandle.NormInputY;      //获取y输入数据
        JumpInput = player.InputHandle.JumpInput;        //获取跳跃输入数据

        // 跳跃状态为真,则切换到跳跃状态
        if (JumpInput)
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
