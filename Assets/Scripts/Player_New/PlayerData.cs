using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("�ٶ�����")]
    public float movementVelocity = 10f;        //�ƶ��ٶ�
    [Header("��Ծ����")]
    public float jumpVelocity = 15f;            //��Ծ�ٶ�
    public int amountOfJump = 1;                //��Ծ����

    [Header("������")]
    public float groundCheckRadius = 0.3f;     //������뾶
    public float wallCheckDistance = 0.5f;     //���ǽ�ھ���
    public LayerMask whatIsGround;       //������㼶

}
