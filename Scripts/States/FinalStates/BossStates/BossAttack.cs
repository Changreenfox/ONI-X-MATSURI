using Godot;
using System;

public class BossAttack : AIMotion
{
	bool finished = false;
	
	public BossAttack(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		host.StateTimer.Start(3.0f);
	}
	
	public override string StateName()
	{
		return "Idle";
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Attack");
	}

	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta);
		if(finished)
		{
			finished = false;
			return "Idle";
		}
		return null;
	}
}
