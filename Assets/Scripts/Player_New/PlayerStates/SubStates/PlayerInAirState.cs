using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;        //�Ƿ��ڵ�����
    
    //������
    private int xInput;     //x����
    private bool grabInput;        //ץȡ����
    private bool JumpInput;     // ��Ծ����
    private bool isJumping;     //�Ƿ���Ծ
    private bool JumpInputStop;      //��Ծ����ֹͣʱ��
    private bool isCoyoteTime;      //����ʱ�� -- ��Ҹ��뿪�����ʱ����Ȼ���Խ�����Ծ

    // ǽ����Ծ����ʱ��
    private float startWallJumpCoyoTime;      //ǽ����Ծ����ʱ��
    private bool wallJumpCoyoteTime;      //ǽ����Ծ����ʱ��

    //ǰ��ǽ�ڼ��
    private bool isTouchingWall;      //�Ƿ�����ǽ��
    private bool isTouchingWallBack;     //�Ƿ���������ǽ��
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;

    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;      //��¼�ϴ��Ƿ�����ǽ��
        oldIsTouchingWallBack = isTouchingWallBack;      //��¼�ϴ��Ƿ���������ǽ��

        isGrounded = player.CheckIfGrounded();      //����Ƿ��ڵ�����
        isTouchingWall = player.CheckIfTouchingWall();     //����Ƿ�����ǽ��
        isTouchingWallBack = player.CheckIfTouchingWallBack();      //����Ƿ���������ǽ��

        // ʹ�û���ʱ��
        if(!wallJumpCoyoteTime && !isTouchingWall&& !isTouchingWallBack &&(oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
        

    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();      //�������ʱ��
        CheckWallJumpCoyoteTime();      //���ǽ������ʱ��

        xInput = player.InputHandle.NormInputX;      //��ȡ������x��������
        JumpInput = player.InputHandle.JumpInput;        //��ȡ��������Ծ��������
        JumpInputStop = player.InputHandle.JumpInputStop;        //��ȡ��������Ծ����ֹͣ����
        grabInput = player.InputHandle.GrabInput;        //��ȡ������ץȡ��������


        CheckJunpMultiplier();

        // ������,���л��� ����״̬ -- LanState
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LanState);
        }

        // ����Ծ���룬�Ҵ�����ǽ�� -- WallJumpState
        else if (JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumCoyoteTime();        // ֹͣǽ����Ծ����ʱ��
            isTouchingWall = player.CheckIfTouchingWall();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);       //����ǽ����Ծ����
            player.stateMachine.ChangeState(player.WallJumpState);      //�л���ǽ����Ծ״̬
        }

        // ��Ұ�����Ծ��,���ҿ�����Ծ,���л�����Ծ״̬ -- JumpState
        else if (JumpInput && player.JumpState.CanJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        }

        // ��Ұ���ץȡ��,���л���ǽ��ץȡ״̬ -- WallGrabState
        else if (isTouchingWall && grabInput)
        {
            player.stateMachine.ChangeState(player.WallGrabState);
        }

        // ת������ǽ״̬
        else if(isTouchingWall && xInput == player.FacingDirection)
        {
            player.stateMachine.ChangeState(player.WallSlideState);      //�л�����ǽ״̬
        }
       
        //δ���,������ڿ��ƶ�
        else
        {
            player.CheckIfShouldFlip(xInput);       //����Ƿ���Ҫ��ת
            player.SetVelocityX(playerData.movementVelocity * xInput);      //����x���ٶ�;

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

    private void CheckWallJumpCoyoteTime()
    {
        //  && ��������ʱ��
        if (wallJumpCoyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;      //��������ʱ��
            player.JumpState.DecreaseAmountofJump();    // ������Ծ����
        }
    }

    // ��������ʱ��
    public void StartCoyoteTime() => isCoyoteTime = true;
    // ����ǽ������ʱ��
    public void StartWallJumCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoTime = Time.time;
    }

    // ֹͣǽ������ʱ��
    public void StopWallJumCoyoteTime() => wallJumpCoyoteTime = false;


}
