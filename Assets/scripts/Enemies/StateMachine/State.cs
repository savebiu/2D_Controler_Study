using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �˽ű������������ʵ���״̬
 * 
 * ������ٶ���
 * 
 * ����״̬��:
 *      --��¼��ʼʱ��
 *      --��������������
 *      
 * �˳�״̬��:
 *      --�رն���������
 *      
 * ֡�߼�
 * 
 * �����߼�
 */
public class State
{
    protected FiniteStateMachine stateMachine;      //����״̬��
    protected Entity entity;        //����ʵ��
    public float startTime { get; protected set; }      //��ʼʱ��

    protected string animBoolName;      //��������ֵ,��֤ÿ��״̬�����������ö���״̬

    //����״̬ʱ��Ҫ����(��ǰʵ�弴����, ��Ӧ��״̬��, ����״̬)
    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()         //ʹ��virtual�ؼ���,����������д�÷���
    {
        startTime = Time.time;      //ÿ�ν���״̬ʱ����洢��ǰʱ��
        entity.anim.SetBool(animBoolName, true);        //����ʵ��Ķ���������
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()       //�߼�����
    {

    }

    public virtual void PhysicsUpdate()         //�������
    {
        DoChecks();
    }
    public virtual void DoChecks()      //ִ�м��,ÿ������״̬ʱ����ִ�м�⹦��
    {        

    }      
}
