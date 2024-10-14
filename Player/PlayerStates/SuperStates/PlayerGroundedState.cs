using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool grabInput;
    private bool dashInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base (player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = core.collisionSenses.Ground;
        isTouchingWall = core.collisionSenses.WallFront;
    }

    public override void Enter()
    {
        base.Enter();

        player.jumpState.ResetJumps();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;
        JumpInput = player.inputHandler.JumpInput;
        grabInput = player.inputHandler.GrabInput;
        dashInput = player.inputHandler.DashInput;

        if (player.inputHandler.SkillInput)
        {
            stateMachine.ChangeState(player.skillState);
        }
        else if (player.inputHandler.AttackInputs[(int)CombatInputs.Primary])
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        else if (player.inputHandler.AttackInputs[(int)CombatInputs.Secondary])
        {
            stateMachine.ChangeState(player.secondaryAttackState);
        }
        else if (JumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        }
        else if (dashInput && !isTouchingWall)
        {
            if (player.dashState.canDash)
            {
                stateMachine.ChangeState(player.dashState);
            }
            else
            {
                player.dashState.CheckCanDash();
            }
        }
        else if (isTouchingWall && grabInput)
        {
            stateMachine.ChangeState(player.wallGrabState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
