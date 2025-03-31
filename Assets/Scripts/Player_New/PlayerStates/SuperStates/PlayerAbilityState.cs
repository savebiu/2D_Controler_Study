using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;      //是否完成能力
    private bool isGrounded;        //是否在地面上
    public PlayerAbilityState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();      //检测是否在地面上
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 状态转换的前提是 完成能力
        if (isAbilityDone)
        {
            // 如果在地面上,并且没进行跳跃(跳跃启动初期可检测到地面),则切换到Idle状态
            if (isGrounded && player.currentVelocity.y < 0.01f)
            {
                playerStateMachine.ChangeState(player.IdleState);
            }
            // 否则切换到InAir状态
            else
            {
                playerStateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
