using Godot;
using System;

public class Moving : AIMotion
{
	private Vector2[] phases;
	private int phase = 0;
	private bool phaseChanged = false;


	public Moving(Actor _host)
	{
		host = _host;
		
		//2 phases: WalkLeft, WalkRight (Will keep walking left until some condition is met)
		phases = new Vector2[] { new Vector2(-1, 0), new Vector2(1, 0) };
	}
	
	public override void Enter()
	{
		base.Enter();
		phase = 0;
		host.Direction = phases[phase];
	}
	
	
	public override void HandleTimer()
	{
		return;
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
