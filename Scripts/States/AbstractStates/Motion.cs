using Godot;
using System;

// Base class for movement physics
public abstract class Motion : State
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

		//Acts different for player vs AI
		CheckAttack();

		//Update direction based on user input (AIMotion disables this inherently)
		GetInputDirection();

		//If no direction, we slow until motion.x == 0
		if(host.Direction.Equals(Vector2.Zero))
			velocity.x = Mathf.Lerp(velocity.x, 0, 0.15f);
		else
			velocity.x += host.Direction.x * host.Speed;
		
		velocity.x = Mathf.Clamp(velocity.x, -host.MaxSpeed, host.MaxSpeed);
		host.Move(velocity);
		return null;
	}

	protected virtual void GetInputDirection() {}

	protected virtual void CheckAttack() {}

	protected virtual void Attack()
	{
		return;
	}
}
