using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ѣ��ʱ�� float stuntime
 * ���˽Ƕ� Vecyor2 knockbackAngle
 */
[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]
public class D_StunState : ScriptableObject
{
    public float stuntime = 2f;         //ѣ��ʱ��
    public float stunknockbackTime = 0.2f;       //��ʼ��¼����ʱ��
    public float stunknockbackSpeed = 20f;       //�����ٶ�

    public Vector2 knockbackAngle;      //���˽Ƕ�
}
