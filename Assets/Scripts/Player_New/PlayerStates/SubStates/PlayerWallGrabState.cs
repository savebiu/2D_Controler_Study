using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchWallState
{
    public PlayerWallGrabState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 设置速度
        player.SetVelocityX(0f);       //设置水平速度为0
        player.SetVelocityY(0f);       //设置垂直速度为0

        if (yInput > 0)
        {
            player.stateMachine.ChangeState(player.WallClimbState);       //如果有向上的输入，则切换到爬墙状态
        }

        else if(yInput < 0f || !grabInput)
        {
            player.stateMachine.ChangeState(player.WallSlideState);       //如果有向下的输入并且没有抓取输入，则切换到滑墙状态
        }
    }
}
