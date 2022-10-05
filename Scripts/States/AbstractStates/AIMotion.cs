using Godot;
using System;

// Base class for walking & jumping of AI (Most functionality remains in Motion.cs)
public abstract class AIMotion : Motion
{
	public override void Enter()
	{
		base.Enter();
	}

	//Direction is altered in the states since it isn't user input
	protected override void GetInputDirection()
	{
		return;
	}

	//Attacks are called in the states themselves, not user input
	protected override void CheckAttack()
	{
		return;
	}
}
