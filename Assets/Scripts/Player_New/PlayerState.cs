using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;        //玩家
    protected PlayerStateMachine playerStateMachine;      //状态机
    protected PlayerData playerData;      //数据

    protected float startTime;      //开始时间,用于记录在每个状态中的运行时间

    protected string animBoolName;        //动画布尔值

    protected bool isAnimationFinished;       //动画完成状态

    // 构造函数
    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }

    // 进入状态
    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);        //设置动画布尔值
        startTime = Time.time;      //记录状态开始时间
        Debug.Log(animBoolName);
        isAnimationFinished = false;        //动画完成状态
    }

    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    public virtual void DoChecks()
    {

    }

    // 动画触发
    public virtual void AnimationTrigger() { }

    // 动画结束触发
    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;     //动画完成触发
}
