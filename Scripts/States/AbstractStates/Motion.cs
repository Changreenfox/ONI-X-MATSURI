using Godot;
using System;

// Base class for walking & jumping
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
		// Might be better to not make this a state

		CheckAttack();

		//Update direction
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

	protected virtual void GetInputDirection()
	{
		float goRight = Input.GetActionStrength("MoveRight");
		float goLeft = Input.GetActionStrength("MoveLeft");
		float jump = Input.GetActionStrength("Jump");
		Vector2 direction = new Vector2(goRight - goLeft, jump);
		host.Direction = direction;

		//Set facing direction
		if(goRight != goLeft)
		{
			facingRight = goRight > goLeft;
			if(facingRight != host.FacingRight)
			{
				host.FacingRight = facingRight;
				PlayAnimation();
			}
		}
	}

	protected virtual void CheckAttack()
	{
		if (Input.IsActionPressed("Attack"))
			Attack();
	}

	protected virtual void Attack()
	{
		return;
	}
}
