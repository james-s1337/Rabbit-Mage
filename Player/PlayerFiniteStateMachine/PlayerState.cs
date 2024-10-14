using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Core core;

    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerData playerData;

    protected bool isAnimFinished;
    protected bool isExitingState;

    protected float startTime;

    private string animBoolName;

    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;

        core = player.core;
    }

    // Called when entering a state
    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        player.anim.SetBool(animBoolName, true);
        isAnimFinished = false;
        isExitingState = false;
        // Debug.Log(animBoolName);
    }

    // Called when leaving a state
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    // Every frame
    public virtual void LogicUpdate() { }

    // Every physics update
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    // Call from PhysicsUpdate() and Enter()
    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimFinished = true;
}
