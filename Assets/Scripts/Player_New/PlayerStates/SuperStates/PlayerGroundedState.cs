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

        // 如果x输入数据不为0, 则切换到移动状态
        if (Xinput != 0)
        {
            playerStateMachine.ChangeState(player.MoveState);
        }
        // 跳跃状态为真,则切换到跳跃状态
        else if (JumpInput)
        {
            Debug.Log("entro Jump");
            player.InputHandle.CheckJumpInput();
            playerStateMachine.ChangeState(player.JumpState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
