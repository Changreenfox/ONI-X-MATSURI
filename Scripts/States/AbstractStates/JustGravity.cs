using Godot;
using System;

public abstract class JustGravity : State
{
	protected bool facingRight;

	public override void Enter()
	{
		facingRight = host.FacingRight;
		PlayAnimation();
	}

    // Input we're looking for is attacking
	public override string HandlePhysics(float delta)
	{
		Vector2 velocity = host.Velocity;

		velocity.y += host.Gravity;
		velocity.y = Mathf.Min(host.MaxFallSpeed, velocity.y);

		host.Move(velocity);
		return null;
	}
}