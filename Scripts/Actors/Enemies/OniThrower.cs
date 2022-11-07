using Godot;
using System;

public class OniThrower : Enemy
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		alertArea = GetNode<Area2D>("Alert");
		
        //Function similarly to OniBrute, but doesn't have to move to continuously attack
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Alert", new Alert(this, "Throwing"));
		container.SetState("Death", new Death(this));
		container.SetState("Throwing", new ThrowAndRun(this));

		state = container.GetState("Idle");
		state.Enter();
	}

	public override void HandleAlert(KinematicBody2D player)
	{
		alertArea.SetDeferred("Monitoring", false);
		ChangeState("Alert");
	}
}

