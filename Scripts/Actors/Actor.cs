using Godot;
using System;

// The base for the player and enemies
// Contains base functionality for HP, attacking, and taking damage
public class Actor : KinematicBody2D
{

	//Actor Variables
	protected int hp;
	
	//Common Functionality
	
	//Every Actor has a sprite
	protected Sprite character;
	
	//Every Actor has access to gravity
	protected const float GRAVITY = 40;
	
	//Floor Normal... says a floor is anything with a normal angle of ^
	protected Vector2 UP = new Vector2(0, -1);
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//So long as any children call base._Ready() if overriden, all sprites
		//will be found dynamically 
		character = (Sprite)GetNode("Sprite");
	}


	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
