using Godot;
using System;

public class Enemy : Actor
{
	protected Area2D alertArea = null;
	protected Area2D lostArea = null;
	
	protected int scoreValue = 0;

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

	public virtual void HandleLost(KinematicBody2D player)
	{
		return;
	}

	public override void Disable()
	{
		alertArea.SetDeferred("monitoring", false);
		lostArea.SetDeferred("monitoring", false);
		base.Disable();
	}
	
	public override void Die()
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.EnemyDied),
									scoreValue
									);
		base.Die();
	}
}       
