using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * 实体类，此脚本负责管理所有实体的基本属性
 * 
 * 该代码编写所有怪物都拥有的属性
 */
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject AliveGO { get; private set; }

    public float facingDirection { get; private set; }      //怪物的朝向
    public Vector2 velocityWorkSpace;       //速度工作空间

    //[SerializeField]
    [Header("检测")]
    public Transform wallCheck;     //墙壁检测
    public Transform ledgeCheck;        //悬崖检测

    private void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        rb = AliveGO.GetComponent<Rigidbody2D>();
        anim = AliveGO.GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()        //virtual的作用是在基类中允许被重写,在子类中只需要使用override即可覆盖该方法
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    //设置朝向,速度
    public void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    //墙壁检测
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    //悬崖检测
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position,Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
}