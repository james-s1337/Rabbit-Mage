using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region State Variables
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerInAirState inAirState { get; private set; }
    public PlayerLandedState landedState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallGrabState wallGrabState { get; private set; }
    public PlayerWallClimbState wallClimbState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerDashStateV1 dashState { get; private set; }
    public PlayerAttackState primaryAttackState { get; private set; }
    public PlayerAttackState secondaryAttackState { get; private set; }
    public PlayerSkillState skillState { get; private set; }

    [SerializeField]
    private PlayerData playerData;
    #endregion

    #region Components
    public Animator anim { get; private set; }
    public PlayerInputHandler inputHandler { get; private set; }
    public Rigidbody2D body { get; private set; }
    public PlayerInventory inventory { get; private set;}
    public Core core { get; private set; }
    #endregion

    #region Other Variables
    [SerializeField]
    private ParticleSystem jumpParticles;
    [SerializeField]
    private ParticleSystem wallJumpParticles;
    public PlayerAfterImagePool instance;
    #endregion

    

    #region Unity Callback Functions
    private void Awake()
    {
        core = GetComponentInChildren<Core>();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, playerData, "idle");
        moveState = new PlayerMoveState(this, stateMachine, playerData, "move");
        jumpState = new PlayerJumpState(this, stateMachine, playerData, "inAir");
        inAirState = new PlayerInAirState(this, stateMachine, playerData, "inAir");
        landedState = new PlayerLandedState(this, stateMachine, playerData, "landed");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, playerData, "wallSlide");
        wallGrabState = new PlayerWallGrabState(this, stateMachine, playerData, "wallGrab");
        wallClimbState = new PlayerWallClimbState(this, stateMachine, playerData, "wallClimb");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, playerData, "inAir");
        ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, playerData, "ledgeClimbState");
        dashState = new PlayerDashStateV1(this, stateMachine, playerData, "dash");

        primaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");
        secondaryAttackState = new PlayerAttackState(this, stateMachine, playerData, "attack");

        skillState = new PlayerSkillState(this, stateMachine, playerData, "attack");
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        inputHandler = GetComponent<PlayerInputHandler>();
        body = GetComponent<Rigidbody2D>();
        inventory = GetComponent<PlayerInventory>();

        primaryAttackState.SetWeap(inventory.weapons[(int) CombatInputs.Primary]);
        secondaryAttackState.SetWeap(inventory.weapons[(int) CombatInputs.Secondary]);

        skillState.SetWeap(inventory.weapons[(int)CombatInputs.Primary]);

        stateMachine.Initialize(idleState);
    }

    // Runs every frame
    private void Update()
    {
        core.LogicUpdate();
        stateMachine.currentState.LogicUpdate();
    }

    // Runs every physics update
    private void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }
    #endregion

    #region Other Functions
    private void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    private void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void PlayJumpParticles()
    {
        jumpParticles.Play();
    }

    public void PlayWallJumpParticles()
    {
        wallJumpParticles.Play();
    }
   
    #endregion

    // TakeDamage() function
    // Check if shield object is active, if it is, get resistance value from weapData and multiply total damage by that
}
