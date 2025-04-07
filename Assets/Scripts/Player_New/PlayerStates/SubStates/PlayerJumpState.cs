using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJump;        //跳跃次数
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        amountOfJump = playerData.amountOfJump;        //跳跃次数
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocityY(playerData.jumpVelocity);       //跳跃
        isAbilityDone = true;       //跳跃完成
        amountOfJump--;        //减少跳跃次数
        player.InAirState.SetisJumping();        //设置跳跃状态
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

    // 多段跳跃检测
    public bool CanJump()
    {
        if (amountOfJump > 0)
        {
            return true;        //可以跳跃
        }
        else
        {
            return false;       //不能跳跃
        }
    }

    // 减少跳跃次数
    public void DecreaseAmountofJump() => amountOfJump--;        //减少跳跃次数

    // 重置跳跃
    public void ResetAmountofJump() => amountOfJump = playerData.amountOfJump;        //重置跳跃次数
}
