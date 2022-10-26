using Godot;
using System;

public class Enemy : Actor
{
	protected CollisionShape2D alertCollider = null;

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

	public virtual void HandleAlert(KinematicBody2D player)
	{
		return;
	}
}       
