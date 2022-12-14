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
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		scoreValue = 10;
		
		container.SetState("Start", new BossStart(this));
		container.SetState("Idle", new BossIdle(this));
		container.SetState("Death", new BossDeath(this));
		container.SetState("Attack", new BossAttack(this));
		container.SetState("Phase2Attack", new BossPhase2Attack(this));
		container.SetState("Phase2Idle", new BossPhase2Idle(this));
		container.SetState("BossAngry", new BossAngry(this));
		
		container.SetState("FinalDeath", new Death(this));
		
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
		gManager.Signals.EmitSignal(nameof(SignalManager.OniBossDied));
		base.Die();
	}
	
	private void _on_BossDeathTimer_timeout()
	{
		/*gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									"res://Scenes/Win.tscn"
									);
		*/
		//GetTree().ChangeScene("res://Scenes/Win.tscn");
	}
	
	public void AttackShake()
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.OniBossAttacked));
	}
	
}


