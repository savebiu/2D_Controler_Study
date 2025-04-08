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

        // �����ٶ�
        player.SetVelocityX(0f);       //����ˮƽ�ٶ�Ϊ0
        player.SetVelocityY(0f);       //���ô�ֱ�ٶ�Ϊ0

        if (yInput > 0)
        {
            player.stateMachine.ChangeState(player.WallClimbState);       //��������ϵ����룬���л�����ǽ״̬
        }

        else if(yInput < 0f || !grabInput)
        {
            player.stateMachine.ChangeState(player.WallSlideState);       //��������µ����벢��û��ץȡ���룬���л�����ǽ״̬
        }
    }
}
