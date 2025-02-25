using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �ýű��ǹ���Ŀ���״̬
 */

public class IdleState : State
{
    protected D_IdleState stateData;

    [Header("�ж�����")]
    protected bool flipAfterImage;      //�Ƿ�ת
    protected bool isIdleTimeOver;      //����ʱ��
    protected bool isPlayerInMinAgroRange;        //����Ƿ�����С������Χ��

    protected float IdleTime;       //���ٽ���Idle��ʱ��
    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)     //( ��ǰ״̬, ״̬��, )
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0f);     //�ٶ�Ϊ0����
        isIdleTimeOver = false;
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //�������Ƿ�����С������Χ��
        SetRandomIdleTiem();        //�����������ʱ��
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterImage)
        {
            entity.Flip();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + IdleTime)
        {
            isIdleTimeOver = true;
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    //�Ƿ�ת
    public void SetFlipAfterImage(bool flip)
    {
        flipAfterImage = flip;
    }

    //����ʱ�����
    public void SetRandomIdleTiem()
    {
        IdleTime = Random.Range(stateData.minIdleTime, stateData.maxIdleTime);      //����С����ʱ���������ʱ��֮�����
    }
}
