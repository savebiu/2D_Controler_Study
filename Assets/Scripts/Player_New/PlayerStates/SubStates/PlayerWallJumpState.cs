using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;       //墙壁跳跃方向
    public PlayerWallJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("WallJump");
        player.InputHandle.CheckJumpInput();
        player.JumpState.ResetAmountofJump();       //重置跳跃次数
        player.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);       //设置跳跃速度,角度，方向
        player.CheckIfShouldFlip(wallJumpDirection);       //翻转角色面向跳跃方向
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
        //if (isTouchingWall)
        //{
        //    wallJumpDirection = -player.FacingDirection;       //设置面朝方向的反方向
        //}
        //else
        //{
        //    wallJumpDirection = player.FacingDirection;        //设置面朝方向
        //}
        wallJumpDirection = isTouchingWall ? -player.FacingDirection : player.FacingDirection;

    }
}
