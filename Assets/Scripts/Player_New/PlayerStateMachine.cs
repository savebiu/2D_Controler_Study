using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    public PlayerState currentState { get; private set; }       //当前状态
    public PlayerState previousState { get; private set; }      //上一个状态

    /*初始化状态机
     * -- startingState: 初始化当前状态
     * -- Enter(): 进入当前状态
     */
    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    /*改变状态
     * -- currentState.Exit(): 退出当前状态
     * -- previousState = currentState: 当前状态记录为上一个状态
     * -- currentState = newState: 新状态记录为当前状态
     * -- currentState.Enter(): 进入新状态
     */
    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    /*返回上一个状态
     * -- currentState.Exit(): 退出当前状态
     * -- currentState = previousState: 当前状态记录为上一个状态
     * -- currentState.Enter(): 进入当前状态
     */
    public void SwitchToPreviousState()
    {
        currentState.Exit();
        currentState = previousState;
        currentState.Enter();
    }

    public void LogicUpdate()
    {
        currentState.LogicUpdate();
    }

    public void PhysicsUpdate()
    {
        currentState.PhysicsUpdate();
    }
}
