using Godot;
using System;

// Base class for walking & jumping
public abstract class Fly : State
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
		//Acts different for player vs AI
		CheckAttack();

		//Update direction 
		GetInputDirection();

		//HandleMotion
		HandleMotion(host.Speed, host.Speed, delta);

		
		return null;
	}

	protected virtual void HandleMotion(float xSpeed, float ySpeed, float delta = 0)
	{
		Vector2 velocity = host.Velocity;

		//If no direction, we slow until motion == 0
		if(host.Direction.Equals(Vector2.Zero))
		{
			velocity.x = Mathf.Lerp(velocity.x, 0, 0.15f);
			velocity.y = Mathf.Lerp(velocity.y, 0, 0.15f);
		}
		else
		{
			velocity.x += host.Direction.x * xSpeed;
			velocity.y += host.Direction.y * ySpeed;
		}

		//No gravity to worry about for a falling enemy (unless an inherited state wants to add some)
		velocity.y = Mathf.Clamp(velocity.y, -host.MaxFallSpeed, host.MaxFallSpeed);
		
		velocity.x = Mathf.Clamp(velocity.x, -host.MaxSpeed, host.MaxSpeed);
		host.Move(velocity);
	}

	protected virtual void GetInputDirection() {}

	protected virtual void CheckAttack() {}

	protected virtual void Attack()
	{
		return;
	}
}