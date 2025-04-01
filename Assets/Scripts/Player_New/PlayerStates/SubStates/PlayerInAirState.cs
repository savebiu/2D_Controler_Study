using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;        //�Ƿ��ڵ�����
    private int Xinput;     //x����
    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Xinput = player.InputHandle.NormInputX;      //��ȡ������x��������

        // ������,���л��� ����״̬
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LanState);
        }

        //δ���,������ڿ��ƶ�
        else
        {
            player.CheckIfShouldFlip(Xinput);       //����Ƿ���Ҫ��ת
            player.SetVelocityX(playerData.movementVelocity * Xinput);      //����x���ٶ�;

            // ����yVelocity�ٶ�
            player.Anim.SetFloat("yVelocity", player.currentVelocity.y);
            //����xVelocity ����x��0��1֮���ֵ,����ȡ����ֵ
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
