using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool GrabInput { get; private set; }
    public bool SkillInput { get; private set; }
    public bool[] AttackInputs { get; private set; }


    [SerializeField]
    private float inputPollTime = 0.2f; // Wait time before jump input is set to true

    private float jumpInputStartTime; // Time when input for jump started
    // InputAction.CallbackContext gets the state of the input
    // context.ReadValue<Vector2>() Gives the unit value of the direction you are holding down (like GetAxis)
    // context.started is true when u pressed the button/input once
    // context.performed is true while a button/input is held down
    // context.canceled is true when a button/input is released
    private void Start()
    {
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];
    }

    private void Update()
    {
        CheckJumpInputHoldTime();
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.Primary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.Primary] = false;
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInputs[(int)CombatInputs.Secondary] = true;
        }

        if (context.canceled)
        {
            AttackInputs[(int)CombatInputs.Secondary] = false;
        }
    }

    public void OnSkillInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SkillInput = true;
        }

        if (context.canceled)
        {
            SkillInput = false;
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() => JumpInput = false;

    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started) {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
        }

        if (context.canceled)
        {
            DashInput = false;
        }
    }

    public Vector2 GetMouseWorldPos()
    {
        if (Gamepad.current != null)
        {
            return new Vector2(Gamepad.current.leftStick.x.ReadValue(), Gamepad.current.leftStick.y.ReadValue());
        }
        else
        {
            return Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }

    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputPollTime)
        {
            JumpInput = false;
        }
    }
}

public enum CombatInputs
{
    Primary,
    Secondary
}