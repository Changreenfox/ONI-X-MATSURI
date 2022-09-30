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
		string temp = base.HandlePhysics(delta);
		if(Mathf.Abs(velocity.x) < WalkToRunSpeed)
			return "Walk";
		return temp;
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
