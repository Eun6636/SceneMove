using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public PlayerAirState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Update()
    {
        base.Update();

        //¶¥À» ¹âÀ¸¸é ¸ØÃß°Ô
        if (stateMachine.Player.Controller.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.IdleState);  //¶¥À» ¹âÀ¸¸é Idle »óÅÂ·Î ÀüÈ¯
            return;
        }

    }

}