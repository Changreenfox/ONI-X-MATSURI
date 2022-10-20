using Godot;
using System;

public class Jump : Motion
{


	private bool face;

	public Jump(Actor _host)
	{
		host = _host;
	}
	
	public override void Enter()
	{
		base.Enter();
		face = host.FacingRight;
		host.PlaySound("Jump");
		Vector2 velocity = host.Velocity;
		velocity.y -= host.JumpSpeed * host.Direction.y;
		host.Velocity = velocity;
	}

	public override string HandlePhysics(float delta)
	{
		// base here is Motion, which will always return null
		string move = base.HandlePhysics(delta);

		//Update to the opposite Jump Animation
		if(host.FacingRight != face)
		{
			PlayAnimation();
			face = host.FacingRight;
		}

		//Check to see if we are still jumping
		if (host.IsOnFloor())
		{
			if(host.Velocity.Equals(Vector2.Zero))
				return "Idle";
			else if (Mathf.Abs(host.Velocity.x) < host.WalkToRunSpeed)
				return "Walk";
			else
				return "Run";
		}
		return move;
	}

	public override void PlayAnimation()
	{
		if(facingRight)
			host.PlayAnimation("JumpRight");
		else
			host.PlayAnimation("JumpLeft");
	}

	//Will not currently work with Attack function... PlayerFSM would require attack to take an int var saying which attack to use
	protected override void Attack()
	{
		if(host.FacingRight)
			host.Attack(1, "JumpRight");
		else
			host.Attack(1, "JumpLeft");
	}

	public override string StateName()
	{
		return "Jump";
	}
}
