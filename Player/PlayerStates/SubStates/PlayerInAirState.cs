using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;
    private bool isGrounded;
    private bool JumpInput;
    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;
    private bool JumpInputStop;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool grabInput;
    private bool isTouchingLedge;
    private bool dashInput;

    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;

    private float startWallJump;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = core.collisionSenses.Ground;
        isTouchingWall = core.collisionSenses.WallFront;
        isTouchingWallBack = core.collisionSenses.WallBack;
        isTouchingLedge = core.collisionSenses.Ledge;

        if (isTouchingWall && !isTouchingLedge)
        {
            player.ledgeClimbState.SetDetectedPos(player.transform.position);
        }

        if (!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = player.inputHandler.NormInputX;
        JumpInput = player.inputHandler.JumpInput;
        JumpInputStop = player.inputHandler.JumpInputStop;
        grabInput = player.inputHandler.GrabInput;
        dashInput = player.inputHandler.DashInput;

        CheckJumpMult(); // variable jump height

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
        else if (isGrounded && core.movement.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.landedState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            stateMachine.ChangeState(player.ledgeClimbState);
        }
        else if (JumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = core.collisionSenses.WallFront;
            player.wallJumpState.DetermineWallJumpDirection(isTouchingWall);
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (JumpInput && player.jumpState.CanJump())
        {
            coyoteTime = false;
            stateMachine.ChangeState(player.jumpState);
        }
        else if (dashInput && !isTouchingWall)
        {
            if (player.dashState.canDash)
            {
                core.movement.SetVelocityZero();
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
        else if (isTouchingWall && xInput == core.movement.facingDirection && core.movement.currentVelocity.y <= 0)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else
        {
            core.movement.CheckIfShouldFlip(xInput);
            core.movement.SetVelocityX(playerData.movementVelocity * xInput);

            player.anim.SetFloat("yVelocity", core.movement.currentVelocity.y);
            player.anim.SetFloat("xVelocity", Mathf.Abs(core.movement.currentVelocity.x));
        }
    }

    private void CheckJumpMult()
    {
        if (isJumping)
        {
            if (JumpInputStop)
            {
                core.movement.SetVelocityY(core.movement.currentVelocity.y * playerData.jumpHeightMult);
                isJumping = false;
            }
            else if (core.movement.currentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;
            player.jumpState.DecreaseJumps();
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void SetIsJumping() => isJumping = true;

    public void StartWallJumpCoyoteTime() {
        wallJumpCoyoteTime = true;
        startWallJump = Time.time;
    }
    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    private void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time >= startWallJump + playerData.coyoteTime)
        {
            StopWallJumpCoyoteTime();
        }
    }
}
