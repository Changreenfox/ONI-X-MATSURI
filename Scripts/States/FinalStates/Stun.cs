using Godot;
using System;

// After being hit/attacking, wait for 1s
public class Stun : JustGravity
{
	public Stun(Actor _host)
	{
		host = _host;
	}

	//Simply wait 1s and return to approach
	public override void Enter()
	{
		host.Speed = 0;
	}

	public override string HandlePhysics(float delta)
	{
		return base.HandlePhysics(delta);
	}
}
