using Godot;
using System;

public class OniBrute : Enemy
{
	// Called when the node enters the scene tree for the first time.
	private Area2D attackCollider;
	public override void _Ready()
	{
		base._Ready();
		alertArea = GetNode<Area2D>("Alert");
		lostArea = GetNode<Area2D>("Lost");
		attackCollider = (Area2D)GetNode("BeginAttack");
		
		scoreValue = 1;
		
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Alert", new Alert(this, "Approach"));
		container.SetState("Lost", new Lost(this));
		container.SetState("Death", new Death(this));
		container.SetState("Approach", new Approach(this));
		container.SetState("Attacking", new BruteAttack(this));

		state = container.GetState("Idle");
		state.Enter();
	}

	public override void HandleAlert(KinematicBody2D player)
	{
		alertArea.SetDeferred("monitoring", false);
		lostArea.SetDeferred("monitoring", true);
		ChangeState("Alert");
	}

	public override void HandleLost(KinematicBody2D player)
	{
		if(!lostArea.Monitoring)
			return;
		alertArea.SetDeferred("monitoring", true);
		lostArea.SetDeferred("monitoring", false);
		ChangeState("Lost");
	}

	public void OnAttackAreaEntered(KinematicBody2D body)
	{
		if(hp <= 0)
			return;
		attackCollider.SetDeferred("monitoring", false);
		ChangeState("Attacking");
	}

	public override void AfterAttack()
	{
		attackCollider.SetDeferred("monitoring", true);
		if(hp > 0)
			ChangeState("Approach");
	}

	public override void AfterAlert()
	{
		attackCollider.SetDeferred("monitoring", true);
	}

	public override void AfterLost()
	{
		attackCollider.SetDeferred("monitoring", false);
	}

	public override void Disable()
	{
		attackCollider.SetDeferred("monitoring", false);
		base.Disable();
	}
}
