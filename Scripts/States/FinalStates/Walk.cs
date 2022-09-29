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
		string temp = base.HandlePhysics(delta);
		if(-0.15 < velocity.x && velocity.x < 0.15)
			return "Idle";
			
		if(-WalkToRunSpeed > velocity.x || velocity.x > WalkToRunSpeed)
			return "Run";
		return temp;
	}

	protected override void PlayAnimation()
	{
		host.PlayAnimation("Walk");
	}

	public override string StateName()
	{
		return "Walk";
	}
}
