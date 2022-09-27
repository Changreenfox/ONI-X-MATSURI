using Godot;
using System;

public class Jump : Motion
{
	[Export]
	protected float jumpSpeed = 1000;
	private bool facingRight;

	public Jump(Actor _host)
	{
		host = _host;
	}
	
	public override void Enter()
	{
		base.Enter();
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
								host.GetType().Name.ToLower(), 
								"jump");
		facingRight = host.FacingRight;
		velocity.y = -jumpSpeed * direction.y;
		host.PlayAnimation("Jump");
	}

	public override string HandlePhysics(float delta)
	{
		// base here is Motion, which will always return null
		string move = base.HandlePhysics(delta);
		if(host.FacingRight != facingRight)
		{
			host.PlayAnimation("Jump");
			facingRight = host.FacingRight;
		}
		if (host.IsOnFloor())
		{
			if(velocity.Equals(Vector2.Zero))
				return "Idle";
			else
				return "Walk";
		}
		return move;
	}

	//Will not currently work with Attack function... PlayerFSM would require attack to take an int var saying which attack to use
	protected override void Attack()
	{
		host.Attack(1);
	}

	public override string StateName()
	{
		return "Jump";
	}
}
