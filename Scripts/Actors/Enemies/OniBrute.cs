using Godot;
using System;

public class OniBrute : Enemy
{
	// Called when the node enters the scene tree for the first time.
	private Area2D attackCollider;
	public override void _Ready()
	{
		base._Ready();
		Area2D alert = (Area2D)GetNode("Alert");

		alertCollider = (CollisionShape2D)alert.GetNode("AlertCollider");
		attackCollider = (Area2D)GetNode("BeginAttack");
		
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Alert", new Alert(this, "Approach"));
		container.SetState("Death", new Death(this));
		container.SetState("Approach", new Approach(this));
		container.SetState("Attacking", new BruteAttack(this));

		state = container.GetState("Idle");
		state.Enter();
	}

	public override void HandleAlert(KinematicBody2D player)
	{
		alertCollider.SetDeferred("disabled", true);
		ChangeState("Alert");
	}

	public void OnAttackAreaEntered(KinematicBody2D body)
	{
		attackCollider.SetDeferred("monitoring", false);
		ChangeState("Attacking");
	}

	public override void AfterAttack()
	{
		GD.Print("AfterAttack");
		attackCollider.SetDeferred("monitoring", true);
		ChangeState("Approach");
	}
}
