using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;        //���
    protected PlayerStateMachine playerStateMachine;      //״̬��
    protected PlayerData playerData;      //����

    protected float startTime;      //��ʼʱ��,���ڼ�¼��ÿ��״̬�е�����ʱ��

    protected string animBoolName;        //��������ֵ

    protected bool isAnimationFinished;       //�������״̬

    // ���캯��
    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    // ����״̬
    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);        //���ö�������ֵ
        startTime = Time.time;      //��¼״̬��ʼʱ��
        Debug.Log(animBoolName);
        isAnimationFinished = false;        //�������״̬
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    // ��������
    public virtual void AnimationTrigger() { }

    // ������������
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;     //������ɴ���
}
