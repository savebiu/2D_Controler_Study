using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ʵ����ű����˽ű������������ʵ��Ļ�������
 * �����ű�
 * �ô����д���й��ﶼӵ�е�����
 * ��ͬ����:
 *      --����rigibody
 *      --����������Animator
 *      --������aliveGo
 */
public class Entity : MonoBehaviour
{
    public FiniteStateMachine stateMachine;
    public D_Entity entityData;

    public Rigidbody2D rb { get; private set; }     //����
    public Animator anim { get; private set; }      //����������
    public GameObject AliveGO { get; private set; }     //������

    public float facingDirection { get; private set; }      //����ĳ���
    public Vector2 velocityWorkSpace;       //�ٶȹ����ռ�

    //[SerializeField]
    [Header("���")]
    public Transform wallCheck;     //ǽ�ڼ��
    public Transform ledgeCheck;        //���¼��
    public Transform playerCheck;       //��Ҽ��

    public virtual void Start()
    {
        //��ʼ��
        AliveGO = transform.Find("Alive").gameObject;       //��ȡ���д�����
        rb = AliveGO.GetComponent<Rigidbody2D>();       //��AliveGo�������л�ȡ���ǵĸ���
        anim = AliveGO.GetComponent<Animator>();        //��AliveGo�������л�ȡ���ǵĶ���������

        facingDirection = 1;        //Ĭ�ϳ���Ϊ1

        stateMachine = new FiniteStateMachine();

    }

    public virtual void Update()        //virtual���������ڻ�����������д,��������ֻ��Ҫʹ��override���ɸ��Ǹ÷���
    {
        stateMachine.currentState.LogicUpdate();        //����״̬���е�ǰ״̬���߼�����
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();      //����״̬���е�ǰ״̬���������
    }

    //���ó���,�ٶ� 
    public void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);       //�����ռ�����Ϊ(x = ��ǰx�᷽�� * �ٶ�, ��ǰy���ٶ�)
        rb.velocity = velocityWorkSpace;        //���ٶȸ�ֵ����������ƶ�
    }

    //ǽ�ڼ��
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, AliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);       //Raycast(����ʼλ��, ����, ����, ͼ��)
    }

    //���¼��
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }


    /*
     *��Ҽ�������Ҫ�ӽ�ɫ���ֳ���
     *
    */

    //�������Ƿ�����С��޷�Χ��
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.transform.position, AliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    //�������Ƿ�������޷�Χ��
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.transform.position, AliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsPlayer);     
    }


    //��ת
    public virtual void Flip()
    {
        facingDirection *= -1;
        AliveGO.transform.Rotate(0f, 180f, 0f);     //Rotate��Rotation������:Rotate������ڵ�ǰ����ת,Rotation������������������ת
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));        //ǽ�ڼ����
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));        //���¼����
    }
}