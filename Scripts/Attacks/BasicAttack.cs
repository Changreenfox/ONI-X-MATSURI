using Godot;
using System;

public class BasicAttack:Area2D
{
	protected Actor host;
	
	// Attack functionality shared by all attacks

	//Damage
	[Export]
	public int damage = 1;

	//Time it takes for attack to play out
	[Export]
	public float attackTime = 0.2f;

	//Time it takes until the player attacks again
	public float cooldown = 0.5f;

	//Private variables for keeping track of state
	private bool attacking = false;
	private bool attacked = false;
	private float timer = 0;
	
	//The range of the attack
	private CollisionPolygon2D range;
	
	public override void _Ready()
	{
		range = (CollisionPolygon2D)GetNode("Range");
		
		host = (Actor)GetNode("/root/World/Player");

		Connect("area_entered", this, nameof(_on_Attack_area_entered));
		Connect("body_entered", this, nameof(_on_Attack_body_entered));
	}

	//Begin the attack animation
	public void Attack()
	{
		if(!attacked)
		{
			attacking = true;
			attacked = true;
		}
	}
	
	//Update state of attack
	public override void _Process(float delta)
	{
		//Use this function to be collider-safe
		range.SetDeferred("disabled", !attacking);

		//attacking state
		if(attacking)
		{
			timer += delta;
			if(timer >= attackTime)
			{
				attacking = false;
				timer = 0;
			}
		}
		//cooldown state
		if(attacked)
		{
			timer += delta;
			if(timer >= cooldown)
			{
				host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name.ToLower(), 
										"attack");
				attacked = false;
				timer = 0;
			}
		}
	}

	//Detect when an enemy is within range
	public void _on_Attack_body_entered(KinematicBody2D collision)
	{
		Actor enemy = collision as Actor;
		//enemy? means if not null, call enemy.TakeDamage, otherwise do nothing
		enemy?.TakeDamage(damage);
		GD.Print(this.Name, " Hit! ", enemy?.Name);
	}

	public void _on_Attack_area_entered(Area2D collision)
	{
		Actor enemy = collision.GetParent() as Actor;
		enemy?.TakeDamage(damage);
		GD.Print("Hit!");
	}

}
