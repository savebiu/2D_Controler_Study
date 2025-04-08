using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDerection;       //墙壁跳跃方向
    public PlayerWallJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("WallJump");
        player.JumpState.ResetAmountofJump();       //重置跳跃次数
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDerection);       //设置跳跃速度
        player.CheckIfShouldFlip(wallJumpDerection);       //翻转角色面向跳跃方向
        player.JumpState.DecreaseAmountofJump();       //减少跳跃次数
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 由于我们只想设置y轴的跳跃角度,所以x轴使用绝对值就好
        player.Anim.SetFloat("yVelocity", player.currentVelocity.y);
        player.Anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;       //能力完成
        }

    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDerection = -player.FacingDerection;       //设置面朝方向的反方向
        }
        else
        {
            wallJumpDerection = player.FacingDerection;        //设置面朝方向
        }

    }
}
