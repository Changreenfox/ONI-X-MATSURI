using Godot;
using System;

public class BossIdle : OnGround
{
	bool finished = false;
	
	public BossIdle(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		finished = false;
		
		host.StateTimer.Start(3.0f);
	}
	
	public override string HandlePhysics(float delta)
	{
		if(finished)
		{
			finished = false;
			return "Moving";
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

	/*
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override string HandleProcess(float delta)
	{
		base.HandleProcess();
		
		// If the actor still needs to idle
		if(!finishCondition())
		{
			return null;
		}
		
		return "Idle";
	}*/
}
