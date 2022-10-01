using Godot;
using System;

public class Run : OnGround
{
	public Run(Actor _host)
	{
		host = _host;
	}

	public override string HandlePhysics(float delta)
	{
		string move = base.HandlePhysics(delta);
		if(Mathf.Abs(host.Velocity.x) < host.WalkToRunSpeed)
			return "Walk";
		return move;
	}

	public override void PlayAnimation()
	{
		host.PlayAnimation("Run", this);
	}

	public override string StateName()
	{
		return "Run";
	}
}
