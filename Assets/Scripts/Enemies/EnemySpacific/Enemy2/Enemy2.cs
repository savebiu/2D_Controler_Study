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
    private Transform meleeAttackPosition;     //��ս����λ��

    public override void Start()
    {
        base.Start();

        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);     //��Idle״̬���ݸ�״̬��
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);     //��Move״̬���ݸ�״̬��
        playerDetectedState = new E2_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);     //��PlayerDetected״̬���ݸ�״̬��
        meleeAttackState = new E2_MeleeAttackState(this, stateMachine, "meleeAttack", meleeAttackPosition, meleeAttackStateData, this);     //��MeleeAttack״̬���ݸ�״̬��


        stateMachine.Initialize(moveState);     //move��Ϊ��ʼ״̬
    }

    public override void Damage(AttackDetails attackDealis)
    {
        base.Damage(attackDealis);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
