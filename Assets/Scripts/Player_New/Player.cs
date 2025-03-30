using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region ���
    public Rigidbody2D RB { get; private set; }                     //����
    public Animator Anim { get; private set; }                   //����
    public PlayerInputHandle InputHandle { get; private set; }      //�������
    #endregion

    #region ״̬��
    public PlayerStateMachine stateMachine { get; private set; }       //״̬��

    public PlayerIdleState IdleState { get; private set; }             //����״̬
    public PlayerMoveSate MoveState { get; private set; }              //�ƶ�״̬
    public PlayerJumpState JumpState { get; private set; }             //��Ծ״̬
    public PlayerInAirState InAirState { get; private set; }           //����״̬
    public PlayerLanState LanState { get; private set; }               //���״̬

    #endregion

    public int FacingDerection { get; private set; }                   //���� 

    [SerializeField]
    private PlayerData playerData;                                      //�������

    private Vector2 workspace;                                          //�����ռ�
    private Vector2 currentVelocity;                                    //��ǰ�ٶ�

    #region Unity�ص�����
    // ��ʼ��״̬��
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");        //��ʼ��Idle״̬��
        MoveState = new PlayerMoveSate(this, stateMachine, playerData, "move");     //��ʼ��Move״̬��
        JumpState = new PlayerJumpState(this, stateMachine, playerData, "jump");        //��ʼ��Jump״̬��
        InAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");     //��ʼ��InAir״̬��
        LanState = new PlayerLanState(this, stateMachine, playerData, "lan");       //��ʼ��Lan״̬��
    }

    // ��ʼ��״̬
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();    //��ȡ����
        Anim = GetComponent<Animator>();   //��ȡ����
        InputHandle = GetComponent<PlayerInputHandle>();   //��ȡ�������
        FacingDerection = 1;    //���÷���Ϊ��

        stateMachine.Initialize(IdleState); //��ʼ��״̬��ΪIdle״̬

    }

    private void Update()
    {
        currentVelocity = RB.velocity;      //��ȡ��ǰ�ٶ�
        stateMachine.LogicUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    #endregion

    #region �ٶȺ���
    // ˮƽ�ٶȺ���(�ƶ�)
    public void SetVelocityX(float veloicity)
    {
        workspace.Set(veloicity, currentVelocity.y);        // �����ٶ�
        RB.velocity = workspace;        //���ø����ٶ�
        currentVelocity = workspace;    //���õ�ǰ�ٶ�Ϊ�����ռ��е�ֵ
        
    }
    //��ֱ�ٶȺ���(��Ծ)
    public void SetVelocityY(float velocity)
    {
        workspace.Set(currentVelocity.x, velocity);        // �����ٶ�
        RB.velocity = workspace;        //���ø����ٶ�
        currentVelocity = workspace;    //���õ�ǰ�ٶ�Ϊ�����ռ��е�ֵ
    }

    #endregion

    #region ��ת����
    // ��ת����
    public void Flip()
    {
        FacingDerection *= -1;      //�ı䷽��
        transform.Rotate(0.0f, 180.0f, 0.0f);      //��ת
    }
    #endregion

    #region ��麯��
    // ��ת���
    public void CheckIfShouldFlip(int xInput)
    {
        if(xInput !=0 && xInput != FacingDerection)
        {
            Flip();
        }
    }


    #endregion
}
