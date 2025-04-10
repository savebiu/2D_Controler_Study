using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;        // 检测位置
    private Vector2 cornerPos;          // 角落位置
    private Vector2 startPos;           // 起始位置
    private Vector2 stopPos;             // 结束位置
    public PlayerLedgeClimbState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();       //设置速度为0
        player.transform.position = detectedPos;      //设置位置为检测位置
        cornerPos = player.DeterminCornerPosition();       //获取角落位置

        startPos.Set(cornerPos.x - (player.FacingDirection * playerData.startOffset.x), cornerPos.y - playerData.startOffset.y);       //设置起始位置
        stopPos.Set(cornerPos.x - (player.FacingDirection * playerData.stopOffset.x), cornerPos.y - playerData.stopOffset.y);       //设置结束位置

        player.transform.position = startPos;       //设置位置为起始位置
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.SetVelocityZero();       //设置速度为0
        player.transform.position = startPos;       //设置位置为起始位置
    }
    //将传递的位置设置为检测位置
    public void SetDetectedPos(Vector2 pos) => detectedPos = pos;
}
