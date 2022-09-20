using Godot;
using System;
using System.Collections.Generic;

// The base for the player and enemies
// Contains base functionality for HP, attacking, and taking damage
public abstract class Actor : KinematicBody2D
{
	/*=============================================================== Members =======================================================*/
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


	//FSM variables
	protected State state;
	protected StateContainer container = new StateContainer();
	
	
	protected Globals globalsRef;

	/*=============================================================== Methods =======================================================*/
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		globalsRef = (Globals)GetNode("/root/Globals");
		
		//So long as any children call base._Ready() if overriden, all sprites
		//will be found dynamically 
		character = (Sprite)GetNode("Sprite");
		
		foreach (Node node in GetChildren())
		{
			if(node is BasicAttack)
			{
				GD.Print(node.Name);
				attacks.Add((BasicAttack)node);
			}
		}
	}

	public override void _PhysicsProcess(float delta)
	{
		if(!active)
			return;
		string name = state.HandlePhysics(delta);
		if(name != null)
			ChangeState(name);
		
		Vector2 scale = character.Scale;
		
		if(facingRight)
			scale.x = 1;
		else
			scale.x = -1;

		foreach (Node2D node in attacks)
				node.Scale = scale;
		character.Scale = scale;
	}

	public override void _Process(float delta)
	{
		if(!active)
			return;
		state.HandleProcess(delta);
	}

	/*=============================================================== Helpers =======================================================*/
	public void ChangeState(string name)
	{
		GD.Print(name);
		state = container.GetState(name);
		state.Enter();
	}

	public Vector2 GetVelocity()
	{
		return velocity;
	}

	public Vector2 Move(Vector2 newVelocity)
	{
		velocity = MoveAndSlide(newVelocity, UP);
		return velocity;
	}

	// Returns true if the actor survives and false if the actor dies
	public void TakeDamage(int damage)
	{
		hp -= damage;
		GD.Print(hp);
	}

	// Handle the Attack here
	public void Attack(int selection)
	{
		attacks[selection].Attack();
		globalsRef.Signals.EmitSignal(nameof(CustomSignals.PlaySoundSignal), "player", "attack");
	}

	//Will be called in the states, allowing the player to play specific animations
	public void PlayAnimation(string name)
	{
		//Vector2 scale = character.Scale;
		
		//character.Scale = scale;
	}

	public virtual void Die()
	{
		QueueFree();
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
