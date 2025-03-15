using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    public D_PlayerDetected stateData;      //ÒýÈë¼ì²âÊý¾Ý
    
    protected bool isPlayerInMinAgroRange;      //Íæ¼ÒÊÇ·ñÔÚ×îÐ¡¹¥»÷·¶Î§ÄÚ
    protected bool isPlayerInMaxAgroRange;      //Íæ¼ÒÊÇ·ñÔÚ×î´ó¹¥»÷·¶Î§ÄÚ
    protected bool isDetectedLedge;     //¼ì²âÐüÑÂ
    protected bool isDetectedWall;      //¼ì²âÇ½±Ú

    protected bool performCloseRangeAction;        //Ö´ÐÐ½ü¾àÀë¹¥»÷
    protected bool performLongRangeAction;      //Ö´ÐÐÔ¶³Ì¹¥»÷

    public PlayerDetectedState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_PlayerDetected stateDate) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateDate;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();        //¼ì²âÍæ¼ÒÊÇ·ñÔÚ×îÐ¡¹¥»÷·¶Î§ÄÚ
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();        //¼ì²âÍæ¼ÒÊÇ·ñÔÚ×î´ó¹¥»÷·¶Î§ÄÚ
        isDetectedLedge = entity.CheckLedge();        //¼ì²âÐüÑÂ
        isDetectedWall = entity.CheckWall();      //¼ì²âÇ½±Ú
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();      //¼ì²âÍæ¼ÒÊÇ·ñÔÚ½ü¾àÀë¹¥»÷·¶Î§ÄÚ
    }

    //¼ì²âµ½Íæ¼ÒÊµÌåºó½øÈë×´Ì¬
    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0);      //ÉèÖÃËÙ¶ÈÎª0
        performLongRangeAction = false;        //Ö´ÐÐÔ¶¾àÀë¶¯×÷
        //performCloseRangeAction = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //Íæ¼ÒÖ´ÐÐÔ¶¾àÀë¹¥»÷
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            performLongRangeAction = true;
        }

        //TODO£º×ª»»Îª¹¥»÷×´Ì¬
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
