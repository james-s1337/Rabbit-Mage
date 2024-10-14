using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Check Transforms
    public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
    public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
    public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }

    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    #endregion


    public float GroundCheckRadius { get => groundCheckRadius; set => groundCheckRadius = value; }
    public LayerMask WhatIsGround { get => whatIsGround; set => whatIsGround = value; }
    public float WallCheckDist { get => wallCheckDist; set => wallCheckDist = value; }
    public LayerMask WhatIsWall { get => whatIsWall; set => whatIsWall = value; }

    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float wallCheckDist;
    [SerializeField] private LayerMask whatIsWall;

    #region Check Functions
    public bool Ground
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public bool WallFront
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * core.movement.facingDirection, wallCheckDist, whatIsWall);
    }

    public bool WallBack
    {
        get => Physics2D.Raycast(wallCheck.position, Vector2.right * -core.movement.facingDirection, wallCheckDist, whatIsWall);
    }

    public bool Ledge
    {
        get => Physics2D.Raycast(ledgeCheck.position, Vector2.right * core.movement.facingDirection, wallCheckDist, whatIsWall);
    }
    #endregion
}
