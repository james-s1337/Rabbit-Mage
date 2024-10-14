using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int jumpsLeft;

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        jumpsLeft = playerData.amountOfJumps;
    }

    public override void Enter()
    {
        base.Enter();

        player.inputHandler.UseJumpInput();
        core.movement.SetVelocityY(playerData.jumpVelocity);
        player.PlayJumpParticles();
        isAbilityDone = true;
        jumpsLeft--;
        player.inAirState.SetIsJumping();
    }

    public bool CanJump()
    {
        if (jumpsLeft > 0)
        {
            return true;
        }

        return false;
    }

    public void ResetJumps() => jumpsLeft = playerData.amountOfJumps;

    public void DecreaseJumps() => jumpsLeft--;

}
