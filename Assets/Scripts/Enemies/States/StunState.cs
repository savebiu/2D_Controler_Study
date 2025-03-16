using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ѣ��״̬,��ϵ��˹���,һ��ʱ���ָ�
 */
public class StunState : State
{
    D_StunState stateData;
 
    protected bool isStunTimeOver;        //ѣ��ʱ���Ƿ����
    protected bool isGrounded;        //�Ƿ��ڵ�����
    protected bool isMovementStopped;        //�Ƿ�ֹͣ�ƶ�

    protected bool performCloseRangeAction;        //ִ�н����붯��
    protected bool isPlayerInMinAgroRange;        //����Ƿ�����С������Χ��

    public StunState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_StunState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();        //�������Ƿ��ڽ����붯����Χ��
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //�������Ƿ�����С������Χ��
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;     //ѣ��ʱ��δ����
        isMovementStopped = false;      //�ƶ�δֹͣ 
        entity.SetVelocity(stateData.stunknockbackSpeed, stateData.knockbackAngle, entity.lastDamageDirection);     //�����ٶ�
        isGrounded = entity.groundCheck;        //�ǵ�����
    }

    public override void Exit()
    {
        base.Exit();

        entity.ResetStunResistance();       //����ѣ�εֿ�

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        //ѣ��ʱ�����
        if (Time.time >= startTime + stateData.stuntime)
        {
            isStunTimeOver = true;
            
        }

        //����ʱ����� ���� ʱ��ûֹͣ
        if (Time.time >= startTime + stateData.stunknockbackTime && !isMovementStopped)
        {
            isMovementStopped = true;
            entity.SetVelocity(0);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
