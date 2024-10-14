using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashStateV1 : PlayerAbilityState
{
    private bool canDashAttack;
    private bool isTouchingWall;
    private Vector2 mousePos;
    private Vector2 lastImagePos;
    public bool canDash { get; private set; }

    public PlayerDashStateV1(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        canDash = true;
    }

    public override void Enter()
    {  
        base.Enter();

        canDash = false;
        canDashAttack = true;

        mousePos = player.inputHandler.GetMouseWorldPos();
        Vector2 forceDirection = mousePos - (Vector2) player.transform.position; 
        int dashDirection;

        if (forceDirection.x < 0)
        {
            dashDirection = -1;
        }
        else
        {
            dashDirection = 1;
        }

        core.movement.SetVelocity(playerData.dashVelocity, forceDirection, 1);
        core.movement.CheckIfShouldFlip(dashDirection); 
        player.instance.GetFromPool();
        lastImagePos = player.transform.position;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        isTouchingWall = core.collisionSenses.WallFront;
        if (((Vector2) player.transform.position - lastImagePos).magnitude > player.instance.distBetweenImages)
        {
            player.instance.GetFromPool();
            lastImagePos = player.transform.position;
        }

        if (Time.time >= startTime + playerData.dashTime || isTouchingWall)
        {
            isAbilityDone = true;
            core.movement.SetVelocityZero();
            canDashAttack = false;
        }
    }

    public void CheckCanDash()
    {
        if (Time.time >= startTime + playerData.dashCooldown)
        {
            canDash = true;
        }
    }
}
