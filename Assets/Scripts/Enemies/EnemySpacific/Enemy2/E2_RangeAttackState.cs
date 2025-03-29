using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_RangeAttackState : RangeAttackState
{
    Enemy2 enemy;

    public E2_RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // 动画结束
        if (isAnimationFinished)
        {
            Debug.Log("动画结束");
            // 如果玩家在最大攻击范围内执行玩家检测状态
            if (isPlayerInMaxAgroRange)
            {
                Debug.Log("进入玩家检测状态");
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            // 否则执行寻找玩家状态
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();        
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();
    }
}
