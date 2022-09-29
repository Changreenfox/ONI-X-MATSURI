using Godot;
using System;

public class PlayerMovement : Actor
{
	[Signal]
	delegate void PlaySoundSignal(string entityName, string soundName);
	delegate void PlayerMovementX(float distance);
	
	//Movement variables
	[Export]
	protected float ACCEL = 90;
	[Export]
	protected float MAXSPEED = 600;
	
	//Jumping variables
	[Export]
	protected float JUMPSPEED = 1000;
	[Export]
	protected float MAXFALLSPEED = 5000;

	protected float GRAVITY = 40;
	// protected Vector2 UP = new Vector2(0, -1);
	
	//Dynamic Variable Declaration
	[Export]
	protected Vector2 motion = new Vector2(0, 0);
	
	//Animation Variables

	
	// Called when the node enters the scene tree for the first time.
	//public override void _Ready()
	//{
		//Assigns the sprite for every actor
		//base._Ready();
	//}

	public override void _PhysicsProcess(float delta)
	{
		
		//Allows other codes to deactivate player movement easily
		if(!active)
			return;
		
		//Gather inputs
		float goRight = Input.GetActionStrength("MoveRight");
		float goLeft = Input.GetActionStrength("MoveLeft");
		float jump = Input.GetActionStrength("Jump");
		bool attacking = Input.IsActionPressed("Attack");

		if(motion.y < MAXFALLSPEED)
			motion.y += GRAVITY;
		
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
		
		if (IsOnFloor())
		{
			if (attacking)
				Attack();
				//EmitSignal(nameof(PlaySoundSignal), "player", "attack");
				//Game starts with attacking = true
			if (jump > 0)
				motion.y = -JUMPSPEED * jump;
		}
		else
			if (attacking)
				JumpAttack();
				
		motion = MoveAndSlide(motion, UP);
		
		EmitSignal("PlayerMovementX", motion.x);
	}

	public void Attack()
	{
		attacks[0].StartAttack("NormalAttack");
	}

	public void JumpAttack()
	{
		attacks[1].StartAttack("JumpAttack");
	}


//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
