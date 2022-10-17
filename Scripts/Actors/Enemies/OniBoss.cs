using Godot;
using System;

public class OniBoss : Enemy
{
	private AnimationPlayer animations;
	public AnimationPlayer Animations
	{
		get { return animations; }
	}
	
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
		
		animations = GetNode<AnimationPlayer>("AnimationPlayer");
		
		container.SetState("Start", new Start(this));
		container.SetState("Idle", new BossIdle(this));
		container.SetState("Death", new Death(this));
		container.SetState("Attack", new BossAttack(this));
		container.SetState("Phase2Attack", new BossPhase2Attack(this));
		
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
		GetTree().ChangeScene("res://Scenes/Win.tscn");
	}
	
	public void PlaySound(string soundName)
	{
		GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), GetType().Name, soundName);
	}
	
	public void FlashColor(float red, float green, float blue)
	{
		ShaderMaterial mat = character.Material as ShaderMaterial;
		mat.SetShaderParam("flashing", true);
	}
}
