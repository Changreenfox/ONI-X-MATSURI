using Godot;
using System;

public class BossDeath : JustGravity
{
	public BossDeath(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		host.Velocity = new Vector2(0, 0);
	}
	
	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta);
		return null;
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Death");
	}
}
