using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    private Weapon weap;
    private float velocityToSet;
    private bool setVelocity;
    private int xInput;
    private bool shouldCheckFlip;

    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;
        weap.EnterWeapon();
    }

    public override void Exit()
    {
        base.Exit();

        weap.ExitWeapon();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        xInput = player.inputHandler.NormInputX;

        if (shouldCheckFlip)
        {
            core.movement.CheckIfShouldFlip(xInput);
        }

        if (setVelocity)
        {
            core.movement.SetVelocityX(velocityToSet * core.movement.facingDirection);
        }

        if (weap.IsShield() && !player.inputHandler.AttackInputs[(int)CombatInputs.Secondary])
        {
            isAbilityDone = true;
        }
    }

    public void SetWeap(Weapon weap)
    {
        this.weap = weap;
        this.weap.InitializeWeap(this);
    }

    public void SetPlayerVelocity(float velocity)
    {
        core.movement.SetVelocityX(velocity * core.movement.facingDirection);

        velocityToSet = velocity;
        setVelocity = true;
    }

    public void SetFlipCheck(bool value)
    {
        shouldCheckFlip = value;
    }


    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        isAbilityDone = true;
    }

    #endregion
}
