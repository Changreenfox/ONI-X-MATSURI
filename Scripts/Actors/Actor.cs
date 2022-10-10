using Godot;
using System;
using System.Collections.Generic;

// The base for the player and enemies
// Contains base functionality for HP, attacking, and taking damage
public abstract class Actor : KinematicBody2D
{
	/*=============================================================== Members =======================================================*/
	protected GameManager gManager;
	public GameManager GManager
	{
		get{ return gManager; }
	}
	
	// Actor Variables
	[Export]
	protected int hp = 3;
	public int HP
	{
		get{ return hp; }
		set{ hp = value; }
	}
	
	// Common Functionality

	protected bool active = true;
	public bool Active
	{
		get{ return active;}
		set{ active = value;}
	}

	// Velocity & Direction values
	[Export]
	protected Vector2 velocity = new Vector2(0, 0);
	public Vector2 Velocity
	{
		get { return velocity; }
		set { velocity = value; }
	}
	[Export]
	protected Vector2 direction = new Vector2(0, 0);
	public Vector2 Direction
	{
		get { return direction; }
		set { direction = value; }
	}

	[Export]
	protected float speed = 90;
	public float Speed
	{
		get{ return speed; }
		set{ speed = value; }
	}
	[Export]
	protected float maxSpeed = 600;
	public float MaxSpeed
	{
		get{ return maxSpeed; }
		set{ maxSpeed = value; }
	}
	[Export]
	protected float gravity = 40;
	public float Gravity
	{
		get{ return gravity; }
		set{ gravity = value; }
	}
	[Export]
	protected float jumpSpeed = 1000;
	public float JumpSpeed
	{
		get { return jumpSpeed; }
		set { jumpSpeed = value; }
	}
	[Export]
	protected float maxFallSpeed = 5000;
	public float MaxFallSpeed
	{
		get{ return maxFallSpeed; }
		set{ maxFallSpeed = value; }
	}
	[Export]
	protected float walkToRunSpeed = 550;
	public float WalkToRunSpeed 
	{
		get{ return walkToRunSpeed; }
		set{ walkToRunSpeed = value; }
	}

	// Floor Normal... says a floor is anything with a normal angle of ^
	protected Vector2 UP = new Vector2(0, -1);
	
	// Every Actor has a sprite
	[Export]
	protected Sprite character;
	
	// Every Actor can face left or right
	[Export]
	protected bool facingRight = true;
	public bool FacingRight
	{
		get{ return facingRight; }
		set{ facingRight = value; }
	}
	
	//Every Actor has a list of attacks
	[Export]
	public List<BasicAttack> attacks = new List<BasicAttack>();
	[Export]
	public List<ContactDamage> surfaces = new List<ContactDamage>();

	private bool attacking = false;
	public bool Attacking
	{
		get{ return attacking; }
		set{ attacking = value; }
	}


	//FSM variables
	protected State state;
	protected StateContainer container = new StateContainer();
	
	//animation
	protected AnimationPlayer animator;
	public AnimationPlayer Animator
	{
		get{ return animator; }
	}
	
	//Only used in AI currently
	private Timer stateTimer;
	public Timer StateTimer
	{
		get{ return stateTimer; }
		set{ stateTimer = value; }
	}


	/*=============================================================== Methods =======================================================*/
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = (GameManager)GetNode("/root/GameManager");
		//So long as any children call base._Ready() if overriden, all sprites
		//will be found dynamically 
		character = (Sprite)GetNode("Sprite");
		
		animator = (AnimationPlayer)GetNode("AnimationPlayer");

		string timerName = Name + "AttackCooldown";
		Timer timer = new Timer();
		timer.Name = timerName;
		AddChild(timer);
		
		foreach (Node node in GetChildren())
		{
			if(node is BasicAttack)
			{
				GD.Print(Name, " -> ", node.Name);
				attacks.Add((BasicAttack)node);
			}
			else if(node is ContactDamage)
			{
				GD.Print(Name, " -> ", node.Name);
				surfaces.Add((ContactDamage)node);
			}
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		if(!active)
			return;

		// Handle States
		string name = state.HandlePhysics(delta);

		if(name != null)
			ChangeState(name);
		
		if(!attacking)
			FaceAttacks();
		
	}

	public override void _Process(float delta)
	{
		if(!active)
			return;

		string name = state.HandleProcess(delta);

		if(name != null)
			ChangeState(name);
	}

	/*=============================================================== Helpers =======================================================*/
	public void ChangeState(string name)
	{
		GD.Print(name);
		state = container.GetState(name);
		state.Enter();
	}

	public Vector2 Move(Vector2 newVelocity)
	{
		velocity = MoveAndSlide(newVelocity, UP);
		return velocity;
	}

	// Returns true if the actor survives and false if the actor dies
	public void TakeDamage(Attack attack)
	{
		GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
									GetType().Name,
									"Damage"
									);
		hp -= attack.Damage;
		TakeKnockback(attack);
		GD.Print(hp);
	}

	//Called in TakeDamage() to cause knockback
	public virtual void TakeKnockback(Attack attack)
	{
		Vector2 impulse = attack.Impulse;
		Vector2 attackPosition = attack.GlobalPosition;

		int directionX = 1, directionY = -1;
		if(attackPosition.x > GlobalPosition.x)
			directionX = -1;
		if(attackPosition.y < GlobalPosition.y)
		{
			GD.Print(attackPosition.y, " - ", GlobalPosition.y);
			directionY = 1;
		}
		impulse.x *= directionX;
		impulse.y *= directionY;

		GD.Print(impulse);
		velocity = MoveAndSlide(impulse * attack.Damage, UP);
	}

	// Handle the Attack here
	public void Attack(int selection, string prevState)
	{
		attacks[selection].StartAttack(prevState);
	}

	//Will be called in the states, allowing the player to play specific animations
	public string PlayAnimation(string name)
	{
		if(name == "IdleForward"){ //if the player is idle for a long time
			animator.Play(name);
			return name;
		}

		if(attacking)
		{
			foreach(Attack attack in attacks)
				if (attack.Waiting)
				{
					attack.PreviousAnim = name;
					return name;
				}
		}

		 //else play the correct animation
		if(facingRight) name += "Right";
		else name += "Left";
		
		animator.Play(name);

		return name;
	}

	public virtual void Die()
	{
		QueueFree();
	}

	//Flips the Actor's colliders
	public void FaceAttacks()
	{
		Vector2 scale = new Vector2(1,1);
		if(facingRight)
			scale.x = 1;
		else
			scale.x = -1;
		foreach (Node2D node in attacks)
			node.Scale = scale;
		foreach (Node2D node in surfaces)
			node.Scale = scale;
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
