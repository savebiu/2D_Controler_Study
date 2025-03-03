using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * �ýű��ǹ�����ƶ�״̬
 */
public class MoveState : State
{
    protected D_MoveState stateData;        //���ƶ�״̬�е����ƶ�����

    protected bool isDetectingWall;       //���ǽ��
    protected bool isDetectingLedge;       //�������
    protected bool isPlayerInMinAgroRange;        //����Ƿ�����С������Χ��

    //base�������ø���Ĺ��캯��,�����ǵ���State�Ĺ��캯��,��Ϊ���಻��ֱ�Ӽ̳и���Ĺ��캯��
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    //��⺯��
    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
    //����״̬ʱ����
    public override void Enter()        //����ͨ��override��д����״̬
    {
        base.Enter();       //Enter����������ʱҲ���Զ����û�����(State״̬)�е�Enter����
        entity.SetVelocity(stateData.movementSpeed);       //����ʵ���ٶ�


    }
   

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
