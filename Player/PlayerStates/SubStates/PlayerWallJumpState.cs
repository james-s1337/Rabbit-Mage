using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        player.jumpState.ResetJumps();
        core.movement.SetVelocity(playerData.wallJumpVelocity, playerData.wallJumpAngle, wallJumpDirection);
        core.movement.CheckIfShouldFlip(wallJumpDirection);
        player.PlayWallJumpParticles();
        player.jumpState.DecreaseJumps();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        player.anim.SetFloat("yVelocity", core.movement.currentVelocity.y);
        player.anim.SetFloat("xVelocity", Mathf.Abs(core.movement.currentVelocity.x));

        if (Time.time >= startTime + playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -core.movement.facingDirection;
        }
        else
        {
            wallJumpDirection = core.movement.facingDirection;
        }
    }
}
