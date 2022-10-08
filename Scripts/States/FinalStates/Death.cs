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
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySound2DSignal), 
										host.GetType().Name,	//Name of entityName must match name of class (standardize later) 
										"Death",
										host.Position
										);
		host.Die();
	}
	
	public override string StateName()
	{
		return "Death";
	}
}
