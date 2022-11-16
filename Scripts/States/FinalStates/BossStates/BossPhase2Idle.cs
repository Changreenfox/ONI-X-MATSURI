using Godot;
using System;

public class BossPhase2Idle : JustGravity
{
	bool finished = false;
	
	public BossPhase2Idle(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		
		host.Velocity = new Vector2(0, host.Velocity.y);
		finished = false;
		host.StateTimer.Start(3.0f);
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
		host.PlayAnimation("IdleForward");
	}
}
