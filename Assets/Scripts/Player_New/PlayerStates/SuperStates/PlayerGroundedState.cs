using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int Xinput;
    protected int Yinput;

    public bool JumpInput;
    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        Xinput = player.InputHandle.NormInputX;      //��ȡx��������
        Yinput = player.InputHandle.NormInputY;      //��ȡy��������
        JumpInput = player.InputHandle.JumpInput;        //��ȡ��Ծ��������

        // ��Ծ״̬Ϊ��,���л�����Ծ״̬
        if (JumpInput)
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
