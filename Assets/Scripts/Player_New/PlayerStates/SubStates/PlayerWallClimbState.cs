using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClimbState : PlayerTouchWallState
{
    public PlayerWallClimbState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 设置速度
        player.SetVelocityY(playerData.wallClimbVelocity);       //设置垂直速度

        if(yInput != 1)
        {
            player.stateMachine.ChangeState(player.WallGrabState);       //如果没有向上的输入，则切换到抓墙状态
        }
    }
}
