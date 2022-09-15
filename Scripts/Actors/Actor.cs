using Godot;
using System;

// The base for the player and enemies
// Contains base functionality for HP, attacking, and taking damage
public class Actor : KinematicBody2D
{

	// Actor Variables
	[Export]
	protected int hp = 3;
	
	// Common Functionality
	
	// Every Actor has a sprite
	[Export]
	protected Sprite character;
	
	// Every Actor has access to gravity
	[Export]
	protected float GRAVITY = 40;
	
	// Floor Normal... says a floor is anything with a normal angle of ^
	[Export]
	protected Vector2 UP = new Vector2(0, -1);
	
	// Every Actor can face left or right
	[Export]
	protected bool facingRight = true;
	
	//Every Actor has a list of attacks
	[Export]
	public BasicAttack[] attacks;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//So long as any children call base._Ready() if overriden, all sprites
		//will be found dynamically 
		character = (Sprite)GetNode("Sprite");
		
		foreach (Node node in GetChildren())
		{
			if(node is BasicAttack)
				GD.Print(node.Name);
		}
	}

	// Returns true if the actor survives and false if the actor dies
	public bool TakeDamage(int damage)
	{
		hp -= damage;
		return hp <= 0;
	}
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
