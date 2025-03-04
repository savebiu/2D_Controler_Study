using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State Data/LookForPlayer Data")]
public class D_LookForPlayerState : ScriptableObject
{
    public int amountOfTurns = 2;        //转身次数
    public float TimeBetweenTurns = 0.75f;        //转身间隔时间
}
