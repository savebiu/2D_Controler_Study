using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MeleeAttackState : MeleeState
{
    Enemy2 enemy;

    public E2_MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, attackPosition, stateData)
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

        // 如果动画完成
        if (isAnimationFinished)
        {
            if (isPlayerInMinAgroRange)
            //if(!isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else
            {
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }
        /*
                if (isPlayerInMaxAgroRange)
                {
                    stateMachine.ChangeState(enemy.playerDetectedState);
                }
                else if (isPlayerInMinAgroRange)
                {
                    stateMachine.ChangeState(enemy.chargeState);
                }
                else if (!isDetectingLedge || isDetectingWall)
                {
                    stateMachine.ChangeState(enemy.MoveState);
                }*/
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