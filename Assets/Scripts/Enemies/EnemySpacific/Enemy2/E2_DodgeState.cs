using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ����״̬
 */
public class E2_DodgeState : DodgeState
{
    Enemy2 enemy;
    public E2_DodgeState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DodgeState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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

        //entity.SetVelocity()     //�����ٶ�
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // ���ܽ���
        if (isDodgeOver)
        {
            //�����Χ��,��Ϊ��ս����״̬ ת��Ϊ ��ս����״̬
            if (isPlayerInMaxAgroRange && performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }

            //�����Χ��,�Ҳ�Ϊ��ս����״̬ ת��Ϊ Զ�̹���״̬
            else if (isPlayerInMaxAgroRange && !performCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.rangeAttackState);
            }

            //�������Χ תΪѰ�����״̬
            else if (!isPlayerInMaxAgroRange)
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
