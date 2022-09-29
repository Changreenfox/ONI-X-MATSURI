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
		if(Mathf.Abs(velocity.x) < 0.15)
			return "Idle";
			
		if(Mathf.Abs(velocity.x) > WalkToRunSpeed)
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
