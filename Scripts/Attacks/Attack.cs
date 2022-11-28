using Godot;
using System;
using System.Threading.Tasks;

public abstract class Attack : Area2D
{
	// Attack functionality shared by all attacks
	[Export]
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

	protected int attackNumber = 0;
	public int AttackNumber
	{
		get {return attackNumber; }
		set {attackNumber = value; }
	}

	//Stores which animation to continue from after the attack animation is finished
	protected string previousAnim;
	public string PreviousAnim
	{
		get{ return previousAnim; }
		set { previousAnim = value; }
	}
	
	public override void _Ready()
	{
		FindHost();

		//Get the range
		GetRange();
		
		//Connect the attack signals
		Connect("body_entered", this, nameof(_on_Attack_body_entered));
		Connect("area_entered", this, nameof(_on_Attack_area_entered));

		//cooldown timer
		time = (Timer)host.GetNode("AttackCooldown");
	}

	protected void FindHost()
	{
		//Find the host
		Node node = this;
		Node root = GetNode("/root");
		while(node != root && !(node is Actor))
		{
			node = node.GetParent();
		}
		if(node is Actor)
			host = (Actor)node;
		else
		{
			GD.Print("You have messed up the heirarchy in some way");
			return;
		}
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
		enemy?.TakeDamage(damage + host.DamageBoost, GlobalPosition, impulse);
	}

	//Detect when an enemy hitbox is within range
	public virtual void _on_Attack_area_entered(KinematicBody2D collision)
	{
		Actor enemy = collision.GetParent() as Actor;
		//GD.Print(this.Name, " Hit! ", enemy?.Name);
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
	}

	// Cancel the attack functionality and animation
	public virtual void Cancel()
	{
		//Stop the animation (should cancel the attack)
	}
}
