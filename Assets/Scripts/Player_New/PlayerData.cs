using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("速度条件")]
    public float movementVelocity = 10f;        //移动速度
    [Header("跳跃条件")]
    public float jumpVelocity = 15f;            //跳跃速度
}
