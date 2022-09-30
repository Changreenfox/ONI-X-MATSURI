using Godot;
using System;

public class BasicAttack : Attack
{
	//The range of the attack
	private CollisionPolygon2D range;

	public override void GetRange()
	{    
		range = (CollisionPolygon2D)GetNode("Range");
	}


	//Update state of attack
	public override void _Process(float delta)
	{
		//Use this function to be collider-safe
		range.SetDeferred("disabled", !attacking);

		//attacking state
		if(attacking)
		{
			timer += delta;
			if(timer >= attackTime)
			{
				host.Attacking = false;
				attacking = false;
				timer = 0;
				previousState.PlayAnimation();
			}
		}
		//cooldown state
		if(attacked)
		{
			timer += delta;
			if(timer >= cooldown)
			{
				GD.Print("Called -> ", timer);
				attacked = false;
				timer = 0;
			}
		}
	}
}
