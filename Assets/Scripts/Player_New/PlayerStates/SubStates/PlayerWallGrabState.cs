using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchWallState
{
    private Vector2 holdPosition;       //保持位置

    public PlayerWallGrabState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;       //获取当前保持位置
        HoldPosition();       //保持位置
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        HoldPosition();
        if (!isExitingState)
        {
            if (yInput > 0)
            {
                player.stateMachine.ChangeState(player.WallClimbState);       //如果有向上的输入，则切换到爬墙状态
            }

            else if (yInput < 0f || !grabInput)
            {
                player.stateMachine.ChangeState(player.WallSlideState);       //如果有向下的输入并且没有抓取输入，则切换到滑墙状态
            }
        }
        
    }

    public void HoldPosition()
    {
        player.transform.position = holdPosition;       //保持当前位置
        // 设置速度
        player.SetVelocityX(0f);       //设置水平速度为0
        player.SetVelocityY(0f);       //设置垂直速度为0
    }

    
}
