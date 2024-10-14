using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandedState : PlayerGroundedState
{
    public PlayerLandedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            if (xInput != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if (isAnimFinished)
            {
                stateMachine.ChangeState(player.idleState);
            }
        }
        
    }
}
