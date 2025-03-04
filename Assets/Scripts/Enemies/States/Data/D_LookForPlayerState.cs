using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/LookForPlayer Data")]
public class D_LookForPlayerState : ScriptableObject
{
    public int amountOfTurns = 2;        //ת�����
    public float TimeBetweenTurns = 0.75f;        //ת����ʱ��
}
