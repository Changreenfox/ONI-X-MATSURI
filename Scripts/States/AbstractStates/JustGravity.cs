using Godot;
using System;

public abstract class JustGravity : State
{
	protected bool facingRight;
	protected Vector2 velocity;

	public override void Enter()
	{
		facingRight = host.FacingRight;
		PlayAnimation();
	}

	public override string HandlePhysics(float delta)
	{
		velocity = host.Velocity;

		velocity.y += host.Gravity;
		velocity.y = Mathf.Min(host.MaxFallSpeed, velocity.y);

		host.Move(velocity);
		return null;
	}
}
