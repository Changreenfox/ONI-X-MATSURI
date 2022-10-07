using Godot;
using System;

public class BossMotion : AIMotion
{
	private bool finished = false;

	public BossMotion(Actor _host)
	{
		host = _host;
	}
	
	public override void Enter()
	{
		base.Enter();
		finished = false;
		
		RandomNumberGenerator randGenerator = new RandomNumberGenerator();
		randGenerator.Randomize();
		float randomTime = randGenerator.RandfRange(3.0f, 5.0f);
		host.StateTimer.Start(randomTime);
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
	
	public override void HandleTimer()
	{
		finished = true;
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("IdleForward");
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
