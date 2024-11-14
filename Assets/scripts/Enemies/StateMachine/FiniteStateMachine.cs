using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �˽ű������������ʵ���״̬��
 */
public class FiniteStateMachine //�ýű�������Unity����,���Բ���̳�MonoBehaviour
{
    //���ٵ�ǰ״̬
    public State currentState { get; private set; }

    //������Ϸ��ʼʱ������״̬
    public void Initialize(State startingState)
    {
        
        currentState = startingState;       //��ʼ״̬
        currentState.Enter();       //���õ�ǰ״̬��Enter����
    }

    //�ı�״̬
    public void ChangeState(State newState)
    {
        currentState.Exit();        //�˳���һ״̬
        currentState = newState;        //״̬�ı�
        currentState.Enter();
        
    }
}
