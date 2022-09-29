using Godot;
using System;

public class Jump : Motion
{
	[Export]
	protected float jumpSpeed = 1000;

	private bool face;

	public Jump(Actor _host)
	{
		host = _host;
	}
	
	public override void Enter()
	{
		base.Enter();
		face = host.FacingRight;
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
								host.GetType().Name.ToLower(), 
								"jump");
		velocity.y = -jumpSpeed * direction.y;
	}

	public override string HandlePhysics(float delta)
	{
		// base here is Motion, which will always return null
		string move = base.HandlePhysics(delta);
		if(host.FacingRight != face)
		{
			host.PlayAnimation("Jump");
			face = host.FacingRight;
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

	protected override void PlayAnimation()
	{
		host.PlayAnimation("Jump");
	}

	//Will not currently work with Attack function... PlayerFSM would require attack to take an int var saying which attack to use
	protected override void Attack()
	{
		host.Attack(1, "JumpAttack");
	}

	public override string StateName()
	{
		return "Jump";
	}
}
