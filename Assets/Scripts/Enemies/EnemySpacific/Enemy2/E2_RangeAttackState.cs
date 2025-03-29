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

        // ��������
        if (isAnimationFinished)
        {
            Debug.Log("��������");
            // ����������󹥻���Χ��ִ����Ҽ��״̬
            if (isPlayerInMaxAgroRange)
            {
                Debug.Log("������Ҽ��״̬");
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            // ����ִ��Ѱ�����״̬
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
