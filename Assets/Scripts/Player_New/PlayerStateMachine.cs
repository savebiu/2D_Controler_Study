using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    public PlayerState CurrentState { get; private set; }       //当前状态
    public PlayerState previousState { get; private set; }      //上一个状态

    /*初始化状态机
     * -- startingState: 初始化当前状态  
     * -- Enter(): 进入当前状态
     */
    public void Initialize(PlayerState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    /*改变状态
     * -- CurrentState.Exit(): 退出当前状态
     * -- previousState = CurrentState: 当前状态记录为上一个状态
     * -- CurrentState = newState: 新状态记录为当前状态
     * -- CurrentState.Enter(): 进入新状态
     */
    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        previousState = CurrentState;
        CurrentState = newState;
        CurrentState.Enter();
    }

    /*返回上一个状态
     * -- CurrentState.Exit(): 退出当前状态
     * -- CurrentState = previousState: 当前状态记录为上一个状态
     * -- CurrentState.Enter(): 进入当前状态
     */
    public void SwitchToPreviousState()
    {
        CurrentState.Exit();
        CurrentState = previousState;
        CurrentState.Enter();
    }

    public void LogicUpdate()
    {
        CurrentState.LogicUpdate();
    }

    public void PhysicsUpdate()
    {
        CurrentState.PhysicsUpdate();
    }
}
