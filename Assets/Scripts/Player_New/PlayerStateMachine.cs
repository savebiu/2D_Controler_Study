using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{

    public PlayerState currentState { get; private set; }       //��ǰ״̬
    public PlayerState previousState { get; private set; }      //��һ��״̬

    /*��ʼ��״̬��
     * -- startingState: ��ʼ����ǰ״̬
     * -- Enter(): ���뵱ǰ״̬
     */
    public void Initialize(PlayerState startingState)
    {
        currentState = startingState;
        currentState.Enter();
    }

    /*�ı�״̬
     * -- currentState.Exit(): �˳���ǰ״̬
     * -- previousState = currentState: ��ǰ״̬��¼Ϊ��һ��״̬
     * -- currentState = newState: ��״̬��¼Ϊ��ǰ״̬
     * -- currentState.Enter(): ������״̬
     */
    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        previousState = currentState;
        currentState = newState;
        currentState.Enter();
    }

    /*������һ��״̬
     * -- currentState.Exit(): �˳���ǰ״̬
     * -- currentState = previousState: ��ǰ״̬��¼Ϊ��һ��״̬
     * -- currentState.Enter(): ���뵱ǰ״̬
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
