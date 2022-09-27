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
										"enemy_oni",	//Name of entityName must match name of class (standardize later) 
										"death");
		host.Die();
	}

	public override string HandlePhysics(float delta)
	{
		return null;
	}

	public override string StateName()
	{
		return "Death";
	}
}
