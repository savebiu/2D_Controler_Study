using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ʵ���࣬�˽ű������������ʵ��Ļ�������
 * 
 * �ô����д���й��ﶼӵ�е�����
 */
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public GameObject AliveGO { get; private set; }

    public float facingDirection { get; private set; }      //����ĳ���
    public Vector2 velocityWorkSpace;       //�ٶȹ����ռ�

    //[SerializeField]
    [Header("���")]
    public Transform wallCheck;     //ǽ�ڼ��
    public Transform ledgeCheck;        //���¼��

    private void Start()
    {
        AliveGO = transform.Find("Alive").gameObject;
        rb = AliveGO.GetComponent<Rigidbody2D>();
        anim = AliveGO.GetComponent<Animator>();
        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()        //virtual���������ڻ�����������д,��������ֻ��Ҫʹ��override���ɸ��Ǹ÷���
    {
        stateMachine.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    //���ó���,�ٶ�
    public void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    //ǽ�ڼ��
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    //���¼��
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position,Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
}