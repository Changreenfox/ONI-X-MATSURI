using Godot;
using System;

// Will inherit from enemy AI
public class EnemyIdle : AIMotion
{
	private Vector2[] phases;
	private int phase = 0;
	private bool phaseChanged = false;

	private float idleSpeed = 25f;
	private float idleMaxSpeed = 150f;

	private float actualSpeed;
	private float actualMaxSpeed;

	public EnemyIdle(Actor _host)
	{
		host = _host;
		//4 phases: IdleForward, WalkLeft, IdleForward, WalkRight
		phases = new Vector2[] { Vector2.Zero, new Vector2(1, 0), Vector2.Zero, new Vector2(-1, 0) };
		actualSpeed = host.Speed;
		actualMaxSpeed = host.MaxSpeed;
	}
	
	public override void Enter()
	{
		base.Enter();

		//Would normally worry about power-ups, however Player never calls this function
		host.Speed = idleSpeed;
		host.MaxSpeed = idleMaxSpeed;

		host.StateTimer.Start(3);
		phase = 0;
		host.Direction = phases[phase];
	}

	public override void Exit()
	{
		host.Speed = actualSpeed;
		host.MaxSpeed = actualMaxSpeed;
	}

	public override string HandlePhysics(float delta)
	{
		// Update the Direction of the enemy based on the current phase
		if(phaseChanged)
		{
			phaseChanged = false;
			host.Direction = phases[phase];

			//If the phase is 1 or 3, we're moving a direction
			if((phase + 1) % 2 == 0)
			{
				if(phase == 3)
				{
					host.FacingRight = false;
					host.PlayAnimation("WalkLeft");
				}
				else
				{
					host.FacingRight = true;
					host.PlayAnimation("WalkRight");
				}

			}
			else
				host.PlayAnimation("IdleForward");
		}

		return base.HandlePhysics(delta);
	}

	public override string StateName()
	{
		return "Idle";
	}

	public override void PlayAnimation()
	{
		host.PlayAnimation("IdleForward");
	}

	public override void HandleTimer()
	{
		phase = (phase + 1) % 4;
		phaseChanged = true;
	}
}
