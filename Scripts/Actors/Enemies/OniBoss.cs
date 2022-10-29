using Godot;
using System;

public class OniBoss : Enemy
{
	private bool cycled = false;
	public bool Cycled
	{
		get { return cycled; }
		set { cycled = value; }
	}
	
	public int death_counter= 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		container.SetState("Start", new BossStart(this));
		container.SetState("Idle", new BossIdle(this));
		container.SetState("Death", new Death(this));
		container.SetState("Attack", new BossAttack(this));
		container.SetState("Phase2Attack", new BossPhase2Attack(this));
		container.SetState("Phase2Idle", new BossPhase2Idle(this));
		container.SetState("BossAngry", new BossAngry(this));
		
		container.SetState("Motion", new BossMotion(this));
		
		direction.x = -1;

		state = container.GetState("Start");
		state.Enter();
	}
	
	private void _on_BossWall_body_entered(object body)
	{
		
		if(body == this) 
		{
			velocity.x = 0;
			direction.x *= -1;
		}
	}

	//Do nothing
	public override void TakeKnockback(Vector2 collisionPosition, Vector2 impulse)
	{
		return;
	}

	public void ShootBullets()
	{
		attacks[0].StartAttack();
		attacks[1].StartAttack();
	}
	
	public override void Die()
	{
		death_counter++; //increase the number of times he has died
		if(death_counter == 1){//if the boss has only died 1 time, enter stage 2
			state = container.GetState("BossAngry");
			state.Enter();
			hp = 3;
		}
		if(death_counter==2){ //else play die animation and show win screen after a bit
			Timer timer = (Timer)GetNode("BossDeathTimer");
			timer.Start(0.5f);
		}
	}
	private void _on_BossDeathTimer_timeout()
	{
		GetTree().ChangeScene("res://Scenes/Win.tscn");
	}
	
	public void AttackShake()
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.OniBossAttacked));
	}
}


