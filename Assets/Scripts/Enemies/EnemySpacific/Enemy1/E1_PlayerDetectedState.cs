using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;


/* ���״̬ת����
 * δ��⵽ʱenemy1ת��Idle״̬
 * ��⵽ʱenemy1��Idleת��PlayerDetected״̬
 * 
 */
public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateDate)
    {
        this.enemy = enemy;
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

        /*//�����Ҳ�����󹥻���Χ����ת����Idle״̬
        if (!isPlayerInMaxAgroRange)
        {
            enemy.idleState.SetFlipAfterImage(false);       //��ϣ����ת
            stateMachine.ChangeState(enemy.idleState);
        }*/

        //�ڽ�ս��Χ��        
        if (performCloseRangeAction)
        {
            stateMachine.ChangeState(enemy.meleeAttackState);
        }
        
        //���������Զ����,��ִ�г��״̬
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(enemy.chargeState);
        }

        //�������º�ǽ��
        else if (!isDetectedLedge)
        {
            entity.Flip();      //��ת
            stateMachine.ChangeState(enemy.moveState);
        }

        //������˲�����󹥻���Χ��,��ת����Ѱ��״̬
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
