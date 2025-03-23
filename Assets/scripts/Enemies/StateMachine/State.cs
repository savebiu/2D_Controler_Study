using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 此脚本负责管理所有实体的状态
 * 
 * 传入跟踪对象
 * 
 * 进入状态后:
 *      --记录开始时间
 *      --启动动画控制器
 *      
 * 退出状态后:
 *      --关闭动画控制器
 *      
 * 帧逻辑
 * 
 * 物理逻辑
 */
public class State
{
    protected FiniteStateMachine stateMachine;      //跟踪状态机
    protected Entity entity;        //跟踪实体
    public float startTime { get; protected set; }      //开始时间

    protected string animBoolName;      //动画布尔值,保证每个状态都能自行设置动画状态

    //创建状态时需要传入(当前实体即对象, 对应的状态机, 动画状态)
    public State(Entity entity, FiniteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()         //使用virtual关键字,允许子类重写该方法
    {
        startTime = Time.time;      //每次进行状态时都会存储当前时间
        entity.anim.SetBool(animBoolName, true);        //启动实体的动画控制器
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()       //逻辑更新
    {

    }

    public virtual void PhysicsUpdate()         //物理更新
    {
        DoChecks();
    }
    public virtual void DoChecks()      //执行检测,每次启动状态时都会执行检测功能
    {        

    }      
}
