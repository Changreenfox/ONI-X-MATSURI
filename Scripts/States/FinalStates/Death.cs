using Godot;
using System;

public class Death : State
{
	public Death(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		//host.PlayAnimation("Death");
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name,	//Name of entityName must match name of class (standardize later) 
										"Death");
		host.Die();
	}

	public override string HandlePhysics(float delta)
	{
		return null;
	}

	// Called when Process is called in FSM
	public override string HandleProcess(float delta)
	{
		return null;
	}

	public override string StateName()
	{
		return "Death";
	}
}
