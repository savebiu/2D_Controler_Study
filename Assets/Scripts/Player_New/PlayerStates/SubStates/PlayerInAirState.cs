using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;        //�Ƿ��ڵ�����
    private int Xinput;     //x����

    private bool JumpInput;     // ��Ծ����
    private bool isJumping;     //�Ƿ���Ծ
    private bool JumpInputStop;      //��Ծ����ֹͣʱ��
    private bool isCoyoteTime;      //����ʱ�� -- ��Ҹ��뿪�����ʱ����Ȼ���Խ�����Ծ
    private bool isTouchWall;      //�Ƿ�����ǽ��

    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();      //����Ƿ��ڵ�����
        isTouchWall = player.CheckIfTouchingWall();     //����Ƿ�����ǽ��
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

        CheckCoyoteTime();      //�������ʱ��

        Xinput = player.InputHandle.NormInputX;      //��ȡ������x��������
        JumpInput = player.InputHandle.JumpInput;        //��ȡ��������Ծ��������
        JumpInputStop = player.InputHandle.JumpInputStop;        //��ȡ��������Ծ����ֹͣ����


        CheckJunpMultiplier();

        // ������,���л��� ����״̬
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LanState);
        }

        // ��Ұ�����Ծ��,���ҿ�����Ծ,���л�����Ծ״̬
        else if (JumpInput && player.JumpState.CanJump())
        {
            player.InputHandle.CheckJumpInput();        // �����Ծ���룬��ֹһֱ��Ծ
            playerStateMachine.ChangeState(player.JumpState);
        }
        // ת������ǽ״̬
        else if(isTouchWall && Xinput == player.FacingDerection)
        {
            player.stateMachine.ChangeState(player.WallSlideState);      //�л�����ǽ״̬
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

    public void CheckJunpMultiplier()
    {
        // ����Ծ״̬��
        if (isJumping)
        {
            // �ɿ���Ծ��
            if (JumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.jumpHeightMultiplier);       //����y���ٶ�
                isJumping = false;      //������Ծ״̬
            }
            // ��������״̬
            else if (player.currentVelocity.y <= 0f)
            {
                isJumping = false;      //������Ծ״̬
            }
        }
    }
    //������Ծ״̬Ϊ��
    public void SetisJumping() => isJumping = true;      

    // �������ʱ��
    private void CheckCoyoteTime()
    {
        //  && ��������ʱ��
        if (isCoyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            isCoyoteTime = false;      //��������ʱ��
            player.JumpState.DecreaseAmountofJump();    // ������Ծ����
        }
    }

    // ��������ʱ��
    public void StartCoyoteTime() => isCoyoteTime = true;       //��������ʱ��


}
