using Godot;
using System;

public class PlayerMovement : Actor
{
	//Existential variable
	protected bool active = true;
	
	//Movement variables
	protected const float ACCEL = 90;
	protected const float MAXSPEED = 600;
	
	//Jumping variables
	protected const float JUMPSPEED = 1000;
	protected const float MAXFALLSPEED = 5000;
	
	//Dynamic Variable Declaration
	protected Vector2 motion = new Vector2(0, 0);
	
	//Animation Variables
	protected bool facingRight = true;
	
	// Called when the node enters the scene tree for the first time.
	//public override void _Ready()
	//{
		//Assigns the sprite for every actor
		//base._Ready();
	//}

	public override void _PhysicsProcess(float delta)
	{
		//GD.Print(hp);
		
		//Allows other codes to deactivate player movement easily
		if(!active)
			return;
		
		if(motion.y < MAXFALLSPEED)
			motion.y += GRAVITY;
		
		float goRight = Input.GetActionStrength("MoveRight");
		float goLeft = Input.GetActionStrength("MoveLeft");
		
		if (goRight > 0)
		{
			motion.x += ACCEL * (goRight - goLeft);
			if(goLeft <= 0)
				facingRight = true;
		}
				
		if (goLeft > 0)
		{
			motion.x -= ACCEL * (goLeft - goRight);
			if(goRight <= 0)
				facingRight = false;
		}
		
		if(goRight <= 0 && goLeft <= 0)
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
		
		float jump = Input.GetActionStrength("Jump");
		if (IsOnFloor() && jump > 0)
			motion.y = -JUMPSPEED * jump;
		
		motion = MoveAndSlide(motion, UP);
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
