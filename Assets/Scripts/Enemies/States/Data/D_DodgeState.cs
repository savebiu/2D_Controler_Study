using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDodgeStateData", menuName = "Data/State Data/Dodge Data")]
public class D_DodgeState : ScriptableObject
{
    public float dodgeSpeed = 10f;        //�����ٶ�
    public float dodgeTime = 0.2f;     //����ʱ��
    public float dodgeCoolDown = 2f;     //������ȴʱ��
    public Vector2 dodgeAngle;      //���ܽǶ�
}
