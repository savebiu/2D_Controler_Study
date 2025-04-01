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

        // 设置水平速度
        player.SetVelocityX(playerData.movementVelocity * Xinput);

        player.CheckIfShouldFlip(Xinput);       //检查是否需要翻转

        // 如果玩家水平输入为0,则切换到Idle状态
        if (Xinput == 0)
        {
            playerStateMachine.ChangeState(player.IdleState);
        }

        // 转换为加速移动状态
        //else if()
        //{

        //}
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
