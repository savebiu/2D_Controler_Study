using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_ChargeState : ChargeState
{
    private Enemy1 enemy;
    public E1_ChargeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        
        //�ڽ�ս��Χ����ת��������״̬
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }

        //δ��⵽���� || δ��⵽ǽ��
        if (!isDetectingLedge || isDetectingWall)
        {
           
            stateMachine.ChangeState(enemy.lookForPlayerState);
        }

        //���״̬����ʱ
        else if (isChargeTimeOver)
        {           
            //����������С������Χ����ת��������״̬�����Ϊ����״̬
            if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);        
            }
            //������С���Ʒ�Χ����ת����Ѱ�����״̬
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
}
