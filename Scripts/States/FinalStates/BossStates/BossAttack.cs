using Godot;
using System;

public class BossAttack : JustGravity
{
	bool finished = false;
	
	public BossAttack(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		GD.Print("BossAttack");
		host.StateTimer.Start(3.0f);
	}
	
	public override string StateName()
	{
		return "Attack";
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Attack");
	}
	
	public override void HandleTimer()
	{
		finished = true;
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
