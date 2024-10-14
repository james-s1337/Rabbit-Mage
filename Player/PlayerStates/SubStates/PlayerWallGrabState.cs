using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPos;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        holdPos = player.transform.position; 
        HoldPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!isExitingState)
        {
            HoldPosition();

            if (yInput > 0)
            {
                stateMachine.ChangeState(player.wallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }

    private void HoldPosition()
    {
        player.transform.position = holdPos;

        core.movement.SetVelocityX(0f);
        core.movement.SetVelocityY(0f);
    }
}
