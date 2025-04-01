using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isGrounded;        //是否在地面上
    private int Xinput;     //x输入
    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();      //检测是否在地面上
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

        Xinput = player.InputHandle.NormInputX;      //获取控制器x输入数据

        // 玩家落地,则切换到 地面状态
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LanState);
        }

        //未落地,则继续在空移动
        else
        {
            player.CheckIfShouldFlip(Xinput);       //检查是否需要翻转
            player.SetVelocityX(playerData.movementVelocity * Xinput);      //设置x轴速度;

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
}
