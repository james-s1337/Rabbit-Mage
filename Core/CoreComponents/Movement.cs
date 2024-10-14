using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    private Vector2 workspace;
    public Vector2 currentVelocity { get; private set; }
    public Rigidbody2D body { get; private set; }
    public int facingDirection { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        body = GetComponentInParent<Rigidbody2D>();
        facingDirection = 1;
    }

    public void LogicUpdate()
    {
        currentVelocity = body.velocity;
    }

    #region Set Functions
    public void SetVelocityX(float velocity)
    {
        workspace.Set(velocity, currentVelocity.y);
        SetCurrentVelocity();
    }

    public void SetVelocityY(float velocity)
    {
        body.velocity = new Vector2(currentVelocity.x, 0);
        workspace.Set(currentVelocity.x, velocity);
        SetCurrentVelocity();
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        body.velocity = new Vector2(currentVelocity.x, 0);
        angle.Normalize();
        workspace.Set(angle.x * velocity * direction, angle.y * velocity);
        SetCurrentVelocity();
    }
    public void SetVelocityZero()
    {
        body.velocity = Vector2.zero;
        currentVelocity = Vector2.zero;
    }

    public void AddForce(Vector2 direction, float force)
    {
        body.AddForce(direction.normalized * force, ForceMode2D.Impulse);
    }

    private void SetCurrentVelocity()
    {
        body.velocity = workspace;
        currentVelocity = workspace;
    }

    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != facingDirection)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        body.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    #endregion
}
