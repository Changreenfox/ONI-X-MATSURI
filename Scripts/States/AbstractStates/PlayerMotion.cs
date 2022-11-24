using Godot;
using System;

//Base class for player input
public abstract class PlayerMotion : Motion
{
    protected override void GetInputDirection()
	{
		float goRight = Input.GetActionStrength("MoveRight");
		float goLeft = Input.GetActionStrength("MoveLeft");
		float jump = Input.GetActionStrength("Jump");
		Vector2 direction = new Vector2(goRight - goLeft, jump);
		host.Direction = direction;

		//Set facing direction
		if(goRight != goLeft)
		{
			facingRight = goRight > goLeft;
			if(facingRight != host.FacingRight)
			{
				host.FacingRight = facingRight;
				PlayAnimation();
			}
		}
	}

    protected override void CheckAttack()
	{
		if (Input.IsActionPressed("Attack"))
			Attack();
	}
}