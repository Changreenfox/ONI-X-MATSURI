using Godot;
using System;

public class Run : OnGround
{
	public Run(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		base.Enter();
		host.PlayAnimation("Run");
	}

	public override string HandlePhysics(float delta)
	{
		string temp = base.HandlePhysics(delta);
		if(-WalkToRunSpeed < velocity.x && velocity.x < WalkToRunSpeed)
			return "Walk";
		return temp;
	}

	public override string StateName()
	{
		return "Run";
	}
}
