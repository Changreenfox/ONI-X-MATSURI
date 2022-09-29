using Godot;
using System;

public abstract class OnGround : Motion
{
	public float WalkToRunSpeed = 300;
	public override string HandlePhysics(float delta)
	{
		// Base here will be Motion, which will always return null
		string move = base.HandlePhysics(delta);
		if(host.IsOnFloor() && Input.IsActionPressed("Jump"))
			return "Jump";
		return move;
	}

	//Normal Attack
	protected override void Attack()
	{    
		host.Attack(0, "Attack");
		//APPARENTLY THIS THING CRASHES IF entityName STARTS WITH AN UPPERCASE LETTER????
		/*
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name.ToLower(), 
										"attack");
		*/
	}
	
}
