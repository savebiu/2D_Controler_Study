using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;        //是否在地面上
    private int xInput;     //x输入
    private bool grabInput;        //抓取输入
    private bool JumpInput;     // 跳跃输入
    private bool isJumping;     //是否跳跃
    private bool JumpInputStop;      //跳跃输入停止时间
    private bool isCoyoteTime;      //土狼时间 -- 玩家刚离开地面的时候仍然可以进行跳跃
    private bool isTouchingWall;      //是否碰到墙壁
    private bool isTouchingWallBack;     //是否碰到背后墙壁


    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();      //检测是否在地面上
        isTouchingWall = player.CheckIfTouchingWall();     //检测是否碰到墙壁
        // Debug.Log(isTouchingWall);
        isTouchingWallBack = player.CheckIfTouchingWallBack();      //检测是否碰到背后墙壁
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

        CheckCoyoteTime();      //检查土狼时间

        xInput = player.InputHandle.NormInputX;      //获取控制器x输入数据
        JumpInput = player.InputHandle.JumpInput;        //获取控制器跳跃输入数据
        JumpInputStop = player.InputHandle.JumpInputStop;        //获取控制器跳跃输入停止数据
        grabInput = player.InputHandle.GrabInput;        //获取控制器抓取输入数据


        CheckJunpMultiplier();

        Debug.Log($"JumpInput: " + JumpInput);
        // 玩家落地,则切换到 地面状态 -- LanState
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LanState);
        }
        // 有跳跃输入，且触碰到墙壁 -- WallJumpState
        else if (JumpInput && (isTouchingWall || isTouchingWallBack))
        //else if (JumpInput && isTouchingWall)
        {
            Debug.Log("满足条件WallJump");
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);       //设置墙壁跳跃方向
            player.stateMachine.ChangeState(player.WallJumpState);      //切换到墙壁跳跃状态
        }

        // 玩家按下跳跃键,并且可以跳跃,则切换到跳跃状态 -- JumpState
        else if (JumpInput && player.JumpState.CanJump())
        {
            player.InputHandle.CheckJumpInput();        // 检查跳跃输入，防止一直跳跃
            playerStateMachine.ChangeState(player.JumpState);
        }
        // 玩家按下抓取键,则切换到墙壁抓取状态 -- WallGrabState
        else if (isTouchingWall && grabInput)
        {
            player.stateMachine.ChangeState(player.WallGrabState);
        }

        // 转换到滑墙状态
        else if(isTouchingWall && xInput == player.FacingDirection)
        {
            player.stateMachine.ChangeState(player.WallSlideState);      //切换到滑墙状态
        }
       
        //未落地,则继续在空移动
        else
        {
            player.CheckIfShouldFlip(xInput);       //检查是否需要翻转
            player.SetVelocityX(playerData.movementVelocity * xInput);      //设置x轴速度;

            // 设置yVelocity速度
            player.Anim.SetFloat("yVelocity", player.currentVelocity.y);
            //设置xVelocity 由于x是0到1之间的值,所以取绝对值
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public void CheckJunpMultiplier()
    {
        // 在跳跃状态下
        if (isJumping)
        {
            // 松开跳跃键
            if (JumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.jumpHeightMultiplier);       //设置y轴速度
                isJumping = false;      //重置跳跃状态
            }
            // 处于下落状态
            else if (player.currentVelocity.y <= 0f)
            {
                isJumping = false;      //重置跳跃状态
            }
        }
    }
    //设置跳跃状态为真
    public void SetisJumping() => isJumping = true;      

    // 检查土狼时间
    private void CheckCoyoteTime()
    {
        //  && 超出土狼时间
        if (isCoyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            isCoyoteTime = false;      //重置土狼时间
            player.JumpState.DecreaseAmountofJump();    // 减少跳跃次数
        }
    }

    // 启动土狼时间
    public void StartCoyoteTime()
    {
        isCoyoteTime = true;
        startTime = Time.time;  // 记录当前时间
    }


}
