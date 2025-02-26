using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * �˽ű������������ʵ���״̬��
 *      --��ʼ״̬
 *      --״̬ת��
 */
public class FiniteStateMachine //�ýű�������Unity����,���Բ���̳�MonoBehaviour
{
    public State currentState { get; private set; }         //��ǰ״̬

    //������Ϸ��ʼʱ������״̬
    public void Initialize(State startingState)
    {
        
        currentState = startingState;       //�����ʼ״̬
        currentState.Enter();       //���õ�ǰ״̬��Enter����
    }

    //�ı�״̬
    public void ChangeState(State newState)
    {
        currentState.Exit();        //�˳���ǰ״̬
        currentState = newState;        //״̬�ı�
        currentState.Enter();
        
    }
}
