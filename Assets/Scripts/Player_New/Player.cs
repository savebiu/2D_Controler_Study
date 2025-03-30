using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D RB { get; private set; }                     //����
    public Animator Anim { get; private set; }                   //����
    public PlayerInputHandle InputHandle { get; private set; }      //�������

    public PlayerStateMachine stateMachine { get; private set; }       //״̬��

    public PlayerIdleState IdleState { get; private set; }             //����״̬
    public PlayerMoveSate MoveState { get; private set; }              //�ƶ�״̬

    [SerializeField]
    private PlayerData playerData;                                      //�������

    private Vector2 workspace;                                          //�����ռ�
    private Vector2 currentVelocity;                                    //��ǰ�ٶ�


    // ��ʼ��״̬��
    private void Awake()
    {
        stateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        MoveState = new PlayerMoveSate(this, stateMachine, playerData, "move");
    }

    // ��ʼ��״̬
    private void Start()
    {
        RB = GetComponent<Rigidbody2D>();    //��ȡ����
        Anim = GetComponent<Animator>();   //��ȡ����
        InputHandle = GetComponent<PlayerInputHandle>();   //��ȡ�������

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

    // �ٶȺ���
    public void SetVelocityX(float veloicity)
    {
        workspace.Set(veloicity, currentVelocity.y);        // �����ٶ�
        RB.velocity = workspace;        //���ø����ٶ�
        currentVelocity = workspace;    //���õ�ǰ�ٶ�Ϊ�����ռ��е�ֵ
    }
}
