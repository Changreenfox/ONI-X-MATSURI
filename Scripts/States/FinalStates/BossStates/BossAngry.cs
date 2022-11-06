using Godot;
using System;

public class BossAngry : JustGravity
{
	bool finished = false;
	
	public BossAngry(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		
		host.Velocity = new Vector2(0, host.Velocity.y);
		finished = false;
		host.StateTimer.Start(2.0f);
	}
	
	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta);
		if(finished)
		{
			return "Phase2Attack";
		}
		return null;
	}
	
	public override void HandleTimer()
	{
		finished = true;
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Angry");
	}
}
