using Godot;
using System;

public class BossIdle : OnGround
{
	Func<Boolean> finishCondition;
	
	
	public BossIdle(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
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
