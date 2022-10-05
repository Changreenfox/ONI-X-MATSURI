using Godot;
using System;

public class Enemy : Actor
{
	//Enemy Functionality
	public override void _Ready()
	{
		base._Ready();

		StateTimer = (Timer)GetNode("StateTimer");
	}

	public void HandleTimer()
	{
		state.HandleTimer();
	}
}       
