using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerAbilityState
{
    private Weapon weap;
    private float velocityToSet;
    private bool setVelocity;
    private int xInput;
    private bool shouldCheckFlip;

    public PlayerSkillState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        setVelocity = false;
        weap.EnterSkill();
    }

    public override void Exit()
    {
        base.Exit();
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

    public void SetWeap(Weapon weap)
    {
        this.weap = weap;
        this.weap.InitializeWeapSkill(this);
    }

    public void FinishCombo()
    {
        isAbilityDone = true;
    }

    #region Animation Triggers

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        weap.ContinueSkill();
    }
    #endregion
}
