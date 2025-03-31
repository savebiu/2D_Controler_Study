using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool isAbilityDone;      //�Ƿ��������
    private bool isGrounded;        //�Ƿ��ڵ�����
    public PlayerAbilityState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = player.CheckIfGrounded();      //����Ƿ��ڵ�����
    }

    public override void Enter()
    {
        base.Enter();
        isAbilityDone = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        // ״̬ת����ǰ���� �������
        if (isAbilityDone)
        {
            // ����ڵ�����,����û������Ծ(��Ծ�������ڿɼ�⵽����),���л���Idle״̬
            if (isGrounded && player.currentVelocity.y < 0.01f)
            {
                playerStateMachine.ChangeState(player.IdleState);
            }
            // �����л���InAir״̬
            else
            {
                playerStateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
