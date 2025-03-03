using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 该脚本是怪物的移动状态
 */
public class MoveState : State
{
    protected D_MoveState stateData;        //在移动状态中调用移动数据

    protected bool isDetectingWall;       //检测墙壁
    protected bool isDetectingLedge;       //检测悬崖
    protected bool isPlayerInMinAgroRange;        //玩家是否在最小攻击范围内

    //base用来调用父类的构造函数,这里是调用State的构造函数,因为子类不能直接继承父类的构造函数
    public MoveState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_MoveState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }
    //检测函数
    public override void DoChecks()
    {
        base.DoChecks();
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
    //进入状态时调用
    public override void Enter()        //可以通过override重写基类状态
    {
        base.Enter();       //Enter函数被调用时也会自动调用基类中(State状态)中的Enter函数
        entity.SetVelocity(stateData.movementSpeed);       //设置实体速度


    }
   

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
