using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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

        // 速度下降(由于是下降，值为负)
        player.SetVelocityY(-playerData.wallSlideVelocity);

        if(yInput == 0f && grabInput && !isExitingState)
        {
            player.stateMachine.ChangeState(player.WallGrabState);       //如果有向下的输入并且有抓取输入，则切换到抓墙状态
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
