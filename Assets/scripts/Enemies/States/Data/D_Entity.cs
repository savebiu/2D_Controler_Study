using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;      //ǽ�������
    public float ledgeCheckDistance = 0.4f;     //���¼�����


    public float minAgroDistance = 3f;      //��С��޾���
    public float maxAgroDistance = 4f;      //����޾���

    //����������
    public float closeRangeActionDistance = 1f;
    public float longRangeActionDistance = 5f;

    public LayerMask whatIsGround;      //����ͼ��
    public LayerMask whatIsPlayer;      //��ɫͼ��
}
