using Godot;
using System;
using System.Threading.Tasks;

public abstract class Attack : Area2D
{
	// Attack functionality shared by all attacks
	protected Actor host;

	//Damage and knockback
	[Export]
	protected int damage = 1;
	public int Damage
	{
		get { return damage; }
		set { damage = value; }
	}
	[Export]
	protected Vector2 impulse = new Vector2(750, 500);
	public Vector2 Impulse
	{
		get { return impulse; }
	}

	//States
	protected bool active = false;
	protected bool attacking = false;
	protected bool waiting = false;
	public bool Waiting
	{
		get { return waiting; }
	}

	[Export]
	protected string name = "Attack";

	//Same Cooldown timer
	protected Timer time;

	[Export]
	protected float cooldown = 0.25f;

	//Stores which animation to continue from after the attack animation is finished
	protected string previousAnim;
	public string PreviousAnim
	{
		get{ return previousAnim; }
		set { previousAnim = value; }
	}
	
	public override void _Ready()
	{
		GetRange();
		
		host = (Actor)GetParent();
		
		Connect("body_entered", this, nameof(_on_Attack_body_entered));

		//cooldown timer
		time = (Timer)host.GetNode("AttackCooldown");
	}

	//Update state of attack
	public override void _Process(float delta)
	{
		//Leave when not ready to call functions
		if(!active)
			return;
		
		//Immediately reset active so we only call it once
		active = false;

		//Call async functions
		ActivateCollider();
		//waiting should be false now
	}

	//Begin the attack animation
	public virtual void StartAttack(string prevAnim = "")
	{
		return;
	}

	//Detect when an enemy is within range
	public virtual void _on_Attack_body_entered(KinematicBody2D collision)
	{
		Actor enemy = collision as Actor;
		//enemy? means if not null, call enemy.TakeDamage, otherwise do nothing
		GD.Print(this.Name, " Hit! ", enemy?.Name);
		enemy?.TakeDamage(damage + host.DamageBoost, GlobalPosition, impulse);
	}

	protected virtual void GetRange() {}
	protected virtual void SetColliderActive() {}

	protected virtual async void ActivateCollider() { await WaitCooldown(); }
	protected virtual async Task WaitCooldown() 
	{ 
		//Wait until timer is timed-out
		time.Start(cooldown);
		await ToSignal(time, "timeout");
		time.Stop();

		waiting = false;
		GD.Print("Cooldown");
	}
}
