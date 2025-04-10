﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("速度条件")]
    public float movementVelocity = 10f;        //移动速度

    [Header("跳跃条件")]
    public float jumpVelocity = 15f;            //跳跃速度
    public float wallJumpVelocity = 20f;   //墙壁跳跃速度
    public float jumpHeightMultiplier = 0.5f;       //跳跃高度乘数
    public int amountOfJump = 2;                //跳跃次数
    public float coyoteTime = 0.1f;        //土狼时间
    public float wallJumpTime = 0.4f;        //墙壁跳跃时间
    public Vector2 wallJumpAngle = new Vector2(1, 2);        //墙壁跳跃角度


    [Header("地面状态")]
    public float groundCheckRadius = 0.3f;     //检测地面半径
    public LayerMask whatIsGround;       //检测地面层级

    [Header("墙壁状态")]
    public float wallCheckDistance = 0.5f;     //检测墙壁距离 
    public float wallSlideVelocity = 3f;        //滑墙速度
    public float wallClimbVelocity = 3f;        //攀爬速度

    [Header("边缘攀爬状态")]
    public Vector2 startOffset;        //起始偏移量
    public Vector2 stopOffset;          //结束偏移量

}
