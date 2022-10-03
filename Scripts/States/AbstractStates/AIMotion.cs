using Godot;
using System;

// Base class for walking & jumping
public abstract class AIMotion : State
{
    protected bool facingRight;

    public override void Enter()
	{
		facingRight = host.FacingRight;
		PlayAnimation();
	}

    public override string HandlePhysics(float delta)
    {
        Vector2 velocity = host.Velocity;
		velocity.y += host.Gravity;
		velocity.y = Mathf.Min(host.MaxFallSpeed, velocity.y);

        velocity.x = Mathf.Lerp(velocity.x, 0, 0.15f);

        if(host.HP <= 0)
            return "Death";

        host.Move(velocity);

        return null;
    }
}
