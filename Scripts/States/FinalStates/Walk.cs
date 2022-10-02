using Godot;
using System;

public class Walk : OnGround
{
	public Walk(Actor _host)
	{
		host = _host;
	}

	public override string HandlePhysics(float delta)
	{
		string move = base.HandlePhysics(delta);
		if(Mathf.Abs(host.Velocity.x) < 0.15)
			return "Idle";
			
		if(Mathf.Abs(host.Velocity.x) > host.WalkToRunSpeed)
			return "Run";
		return move;
	}

	public override void PlayAnimation()
	{
		host.PlayAnimation("Walk");
	}

	public override string StateName()
	{
		return "Walk";
	}
}
