using Godot;
using System;

public class OniBrute : Enemy
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		Area2D alert = (Area2D)GetNode("Alert");

		alertCollider = (CollisionShape2D)alert.GetNode("AlertCollider");
		
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Alert", new Alert(this));
		container.SetState("Death", new Death(this));
		container.SetState("Approach", new Approach(this));

		state = container.GetState("Idle");
		state.Enter();
	}

	public override void HandleAlert(KinematicBody2D player)
	{
		alertCollider.SetDeferred("disabled", true);
		state.Exit();
		ChangeState("Alert");
	}
}
