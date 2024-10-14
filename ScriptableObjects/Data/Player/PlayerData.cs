using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 7f;
    public int amountOfJumps = 2;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 8f;
    public float wallJumpTime = 0.4f;
    public Vector2 wallJumpAngle = new Vector2(1f, 2f);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float jumpHeightMult = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset = new Vector2();
    public Vector2 stopOffset = new Vector2();

    [Header("Dashing State")]
    public float dashVelocity = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = 2f;

    [Header("Vitals")]
    public int health = 100;
    public int stamina = 100;
    public int magikCapacity = 50;
}
