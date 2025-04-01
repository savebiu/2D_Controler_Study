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
    public int amountOfJump = 1;                //跳跃次数

    [Header("地面检测")]
    public float groundCheckRadius = 0.3f;     //检测地面半径
    public float wallCheckDistance = 0.5f;     //检测墙壁距离 
    public LayerMask whatIsGround;       //检测地面层级

}
