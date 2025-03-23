using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *玩家检测状态
 */
public class E2_PlayerDetectedState : PlayerDetectedState
{
    Enemy2 enemy;
    public E2_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateDate)
    {
        this.enemy = enemy;
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

        // 在近战攻击范围内转换为 MeleeState
        if (performCloseRangeAction)
        {
            // 闪避时间 + 冷却时间结束 ，转换为闪避状态
            if (Time.time >= enemy.dodgeState.startTime + enemy.dodgeStateData.dodgeCoolDown)
            {
                stateMachine.ChangeState(enemy.dodgeState);
            }
            else
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
        }
        //不在最大范围内，转换为LookForPlayerState
        else if (!isPlayerInMaxAgroRange)
        {
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }
        

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
