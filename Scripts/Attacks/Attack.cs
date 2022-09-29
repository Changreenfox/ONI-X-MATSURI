using Godot;
using System;

public abstract class Attack : Area2D
{
    // Attack functionality shared by all attacks
    protected Actor host;

    //Damage
	[Export]
	public int damage = 1;

    //Time it takes for attack to play out
	[Export]
	public float attackTime = 0.2f;

	//Time it takes until the player attacks again
	public float cooldown = 0.5f;

	//Private variables for keeping track of state
	protected bool attacking = false;
	protected bool attacked = false;
	protected float timer = 0;
	
	public override void _Ready()
	{
        Node node = GetNode("Range");
        GetRange();
		
		host = (Actor)GetNode("/root/World/Player");

		Connect("area_entered", this, nameof(_on_Attack_area_entered));
		Connect("body_entered", this, nameof(_on_Attack_body_entered));
    }

    //Begin the attack animation
	public virtual void StartAttack()
	{
		if(!attacked)
		{
			attacking = true;
			attacked = true;
			host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name.ToLower(), 
										"attack");
		}
	}

    //Detect when an enemy is within range
	public virtual void _on_Attack_body_entered(KinematicBody2D collision)
	{
		Actor enemy = collision as Actor;
		//enemy? means if not null, call enemy.TakeDamage, otherwise do nothing
        GD.Print(this.Name, " Hit! ", enemy?.Name);
		enemy?.TakeDamage(damage);
	}

	public virtual void _on_Attack_area_entered(Area2D collision)
	{
		Actor enemy = collision.GetParent() as Actor;
		enemy?.TakeDamage(damage);
		GD.Print("Hit!");
	}

    public abstract void GetRange();
}