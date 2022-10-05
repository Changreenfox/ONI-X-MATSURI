using Godot;
using System;

public class Moving : AIMotion
{
	private Vector2[] phases;
	private int phase = 0;
	private bool phaseChanged = false;

	// Called when the node enters the scene tree for the first time.
	public Moving(Actor _host)
	{
		host = _host;
		
		//2 phases: WalkLeft, WalkRight
		phases = new Vector2[] { new Vector2(1, 0), new Vector2(-1, 0) };
	}
	
	public override void Enter()
	{
		base.Enter();
		phase = 0;
		host.Direction = phases[phase];
	}
	
	// Input we're looking for is attacking
	public override string HandlePhysics(float delta)
	{
		// Update the Direction of the enemy based on the current phase
		if(phaseChanged)
		{
			phaseChanged = false;
			host.Direction = phases[phase];

			//If the phase is 1 or 3, we're moving a direction
			if(phase == 0)
			{
				host.FacingRight = false;
			}
			else
			{
				host.FacingRight = true;
			}
		}
		
		return base.HandlePhysics(delta);
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
