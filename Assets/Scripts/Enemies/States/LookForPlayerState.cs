using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

/*
 * Ѱ�����״̬
 */
public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnsDone;
    protected bool isAllTurnsTimeDone;      //���з�תʱ�����
    protected bool turnImmediately;     //������ת

    protected float lastTurnTime;       //�ϴη�תʱ��

    public int amountOfTurnsDone;

    public LookForPlayerState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();
        /*
         * ��ʼ������״̬
         */
        isAllTurnsDone = false;         //���з�תδ���
        isAllTurnsTimeDone = false;     //���з�תʱ��δ����

        lastTurnTime = startTime;       //����ʼʱ������Ϊ�ϴη�תʱ��
        amountOfTurnsDone = 0;          //��ɵķ�ת����Ϊ0
        entity.SetVelocity(0);          //�ٶ�Ϊ0
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (turnImmediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;   
            amountOfTurnsDone++;        //��ת������һ
            turnImmediately = false;     //��ֹ������ת
        }
        else if(!isAllTurnsDone && Time.time >= lastTurnTime + stateData.TimeBetweenTurns)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnsDone++;
        }

        //�ж��Ƿ�������з�ת���
        if(amountOfTurnsDone >= stateData.amountOfTurns)        //��ɷ�ת������ >= �ܷ�ת����
        {
            isAllTurnsDone = true;
        }
        if (isAllTurnsDone && Time.time >= lastTurnTime + stateData.TimeBetweenTurns)      //���з�ת��� && ��ת��ȴ����
        {
            isAllTurnsDone = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void SetTurnImediately(bool flip)
    {
        turnImmediately = flip;
    }
}
