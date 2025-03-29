
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 远距离攻击状态
 */
public class RangeAttackState : AttackState
{
    protected D_RangeAttackState stateData;      //远距离攻击状态数据
    protected GameObject projectile;        //投射物
    protected Projectile projectileScript;       //投射物逻辑脚本

    private bool hasTriggeredAttack = false; // 添加一个标志位来确保 TriggerAttack 只被调用一次


    public RangeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_RangeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        hasTriggeredAttack = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void TriggerAttack()
    {

        base.TriggerAttack();

        Debug.Log("进入远程攻击");
        // 生成投射物
        projectile = GameObject.Instantiate(stateData.projectile, attackPosition.position, attackPosition.rotation);
        projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.FireProjectile(stateData.projectileSpeed, stateData.projectileTravelDistance, stateData.projectileDamage);

        hasTriggeredAttack = true; // 设置标志位，确保 TriggerAttack 只被调用一次
    }
}
