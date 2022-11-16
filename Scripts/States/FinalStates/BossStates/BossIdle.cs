using Godot;
using System;

public class BossIdle : JustGravity
{
	bool finished = false;
	int next_state = -1; //if next state = -1, go into attack state, else go into moving state
	
	public BossIdle(Actor _host) 
	{
		host = _host;
		//finishCondition = condition;
	}
	
	public override void Enter()
	{
		base.Enter();
		
		host.Velocity = new Vector2(0, host.Velocity.y);
		finished = false;
		if(next_state == -1){
			host.StateTimer.Start(3.0f);
		}
		else {
			host.StateTimer.Start(1.0f);
		}
	}
	
	public override string HandlePhysics(float delta)
	{
		base.HandlePhysics(delta);
		if(finished)
		{
			finished = false;
			if(next_state == 1){
				next_state *= -1;
				return "Motion";
			}
			next_state *= -1;
			return "Attack";
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
