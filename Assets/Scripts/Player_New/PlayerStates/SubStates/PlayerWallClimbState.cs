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

        // �����ٶ�
        if (isTouchingWall)
        {
            player.SetVelocityY(playerData.wallClimbVelocity);       //���ô�ֱ�ٶ�
        }

        if (yInput != 1 && !isExitingState)
        {
            player.stateMachine.ChangeState(player.WallGrabState);       //���û�����ϵ����룬���л���ץǽ״̬
        }
    }
}
