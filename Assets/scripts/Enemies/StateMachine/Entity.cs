using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 实体类脚本，此脚本负责管理所有实体的基本属性
 * 基础脚本
 * 该代码编写所有怪物都拥有的属性
 * 共同属性:
 *      --刚体rigibody
 *      --动画控制器Animator
 *      --存活对象aliveGo
 */
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Rigidbody2D rb { get; private set; }     //刚体
    public Animator anim { get; private set; }      //动画控制器
    public GameObject aliveGO { get; private set; }     //存活对象

    public float facingDirection { get; private set; }      //怪物的朝向
    public AnimationToStatemachine atsm { get; private set; }        //动画状态机

    public Vector2 velocityWorkSpace;       //速度工作空间

    //[SerializeField]
    [Header("检测")]
    public Transform wallCheck;     //墙壁检测
    public Transform ledgeCheck;        //悬崖检测
    public Transform playerCheck;       //玩家检测

    public virtual void Start()
    {
        //初始化
        aliveGO = transform.Find("Alive").gameObject;       //获取所有存活对象
        rb = aliveGO.GetComponent<Rigidbody2D>();       //从AliveGo存活对象中获取他们的刚体
        anim = aliveGO.GetComponent<Animator>();        //从AliveGo存活对象中获取他们的动画控制器
        atsm = aliveGO.GetComponent<AnimationToStatemachine>();        //从AliveGo存活对象中获取他们的动画状态机

        facingDirection = 1;        //默认朝向为1

        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()        //virtual的作用是在基类中允许被重写,在子类中只需要使用override即可覆盖该方法
    {
        stateMachine.currentState.LogicUpdate();        //调用状态机中当前状态的逻辑更新
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();      //调用状态机中当前状态的物理更新
    }

    //设置朝向,速度 
    public void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);       //工作空间设置为(x = 当前x轴方向 * 速度, 当前y轴速度)
        rb.velocity = velocityWorkSpace;        //将速度赋值给刚体进行移动
    }

    //墙壁检测
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);       //Raycast(检测初始位置, 方向, 距离, 图层)
    }

    //悬崖检测
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }


    /*
     *玩家检测出发点要从角色部分出发
     *
    */

    //检测玩家是否在最小仇恨范围内
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.transform.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    //检测玩家是否在最大仇恨范围内
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.transform.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);     
    }

    //检查玩家是否在近距离攻击范围内
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.transform.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsPlayer);
    }

    //翻转
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);     //Rotate和Rotation的区别:Rotate是相对于当前的旋转,Rotation是相对于世界坐标的旋转
    }



    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));        //墙壁检测线
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));        //悬崖检测线
    }
}