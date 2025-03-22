using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    //״̬�б�
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }     //��Ҽ��״̬��ȡ��
    public E1_ChargeState chargeState { get; private set; }       //���״̬��ȡ��
    public E1_LookForPlayerState lookForPlayerState { get; private set; }       //Ѱ�����״̬��ȡ��
    public E1_MeleeAttackState meleeAttackState { get; private set; }       //��ս����״̬��ȡ��
    public E1_StunState stunState { get; private set; }       //ѣ��״̬��ȡ��

    //�����б�
    [Header("����")]
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;       //��Ҽ��״̬����
    [SerializeField]
    private D_ChargeState chargeStateData;     //���״̬����
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;        //Ѱ�����״̬����
    [SerializeField]
    private D_MeleeState meleeAttackStateData;       //��ս����״̬����
    [SerializeField]
    private Transform meleeAttackPosition;     //��ս����λ��
    [SerializeField]
    private D_StunState stunStateData;       //ѣ��״̬����

    public override void Start()
    {
        base.Start();

        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);      //�����ƶ�״̬
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);      //��������״̬
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);      //������Ҽ��״̬
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);      //�������״̬
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);      //����Ѱ�����״̬
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);      //������ս����״̬
        stunState = new E1_StunState(this, stateMachine, "stun", stunStateData, this);      //����ѣ��״̬

        stateMachine.Initialize(idleState);     //��ʼ��״̬��
    }    

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if(isStunned && stateMachine.currentState != stunState)
        {
            stateMachine.ChangeState(stunState);
        }
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
    }
}
