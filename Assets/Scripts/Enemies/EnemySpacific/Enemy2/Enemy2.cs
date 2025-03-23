using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    //״̬�б�
    public E2_IdleState idleState { get; private set; }     //����Idle״̬
    public E2_MoveState moveState { get; private set; }     //����Move״̬
    public E2_PlayerDetectedState playerDetectedState { get; private set; }     //����PlayerDetected״̬
    public E2_MeleeAttackState meleeAttackState { get; private set; }     //����MeleeAttack״̬
    public E2_LookForPlayerState lookForPlayerState { get; private set; }     //����LookForPlayer״̬
    public E2_StunState stunState { get; private set; }     //����Stun״̬

    //�����б�
    [Header("����")]
    [SerializeField]
    private D_IdleState idleStateData;     //����Idle״̬����
    [SerializeField]
    private D_MoveState moveStateData;     //����Move״̬����
    [SerializeField]
    private D_PlayerDetected playerDetectedStateData;     //����PlayerDetected״̬����
    [SerializeField]
    private D_MeleeState meleeAttackStateData;     //����MeleeAttack״̬����
    [SerializeField]
    private Transform meleeAttackPosition;     //��ս����λ��
    [SerializeField]
    private D_LookForPlayerState lookForPlayerStateData;     //����LookForPlayer״̬����
    [SerializeField]
    private D_StunState stunStateData;     //����Stun״̬����

    public override void Start()
    {
        base.Start();

        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);     //��Idle״̬���ݸ�״̬��
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);     //��Move״̬���ݸ�״̬��
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);     //��PlayerDetected״̬���ݸ�״̬��
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);     //��MeleeAttack״̬���ݸ�״̬��
        lookForPlayerState = new E2_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);     //��LookForPlayer״̬���ݸ�״̬��
        stunState = new E2_StunState(this, stateMachine, "stun", stunStateData, this);     //��Stun״̬���ݸ�״̬��

        stateMachine.Initialize(moveState);     //move��Ϊ��ʼ״̬
    }

    public override void Damage(AttackDetails attackDealis)
    {
        base.Damage(attackDealis);

        // ��ѣ�β��Ҳ���ѣ��״̬
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
