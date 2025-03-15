using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_StunState : StunState
{
    Enemy1 enemy;

    public E1_StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        if (isStunTimeOver)
        {
            //�ڽ�ս��Χ��
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);       //ת��Ϊ ��ս����״̬
            }
            //����С������Χ
            else if (isPlayerInMinAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);        //ת��Ϊ ���״̬
            }
            //Ѱ��
            else
            {
                enemy.lookForPlayerState.SetTurnImediately(true);       //����ת��
                stateMachine.ChangeState(enemy.lookForPlayerState);      //ת��Ϊ Ѱ�����״̬
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
