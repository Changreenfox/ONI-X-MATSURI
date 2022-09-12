using Godot;
using System;

public class PlayerMovement : KinematicBody2D
{
	//Existential variable
	public bool active = true;
	
	//Movement variables
	private const float ACCEL = 90;
	private const float MAXSPEED = 600;
	
	//Jumping variables
	private const float GRAVITY = 40;
	private const float JUMPSPEED = 1000;
	private const float MAXFALLSPEED = 5000;
	
	//Floor Normal... says a floor is anything with a normal angle of ^
	private Vector2 UP = new Vector2(0, -1);
	
	//Dynamic Variable Declaration
	private Vector2 motion = new Vector2(0, 0);
	private Sprite character;
	
	//Animation Variables
	private bool facingRight = true;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		character = (Sprite)GetNode("Sprite");
	}

	public override void _PhysicsProcess(float delta)
	{
		if(motion.y < MAXFALLSPEED)
			motion.y += GRAVITY;
		
		bool goRight = Input.IsActionPressed("MoveRight");
		bool goLeft = Input.IsActionPressed("MoveLeft");
		
		if (goRight)
		{
			motion.x += ACCEL;
			if(!goLeft)
				facingRight = true;
		}
				
		if (goLeft)
		{
			motion.x -= ACCEL;
			if(!goRight)
				facingRight = false;
		}
		
		if(!goRight && !goLeft)
			motion.x = Mathf.Lerp(motion.x, 0, 0.15f);
			
		if (facingRight)
		{
			Vector2 scale = character.Scale;
			scale.x = 1;
			character.Scale = scale;
		}
		
		else
		{
			Vector2 scale = character.Scale;
			scale.x = -1;
			character.Scale = scale;
		}
		
		motion.x = Mathf.Clamp(motion.x, -MAXSPEED, MAXSPEED);
		
		if (IsOnFloor() && Input.IsActionPressed("Jump"))
			motion.y = -JUMPSPEED;
		
		motion = MoveAndSlide(motion, UP);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
