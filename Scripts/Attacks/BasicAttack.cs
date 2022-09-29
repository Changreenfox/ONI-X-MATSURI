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
				attacking = false;
				timer = 0;
			}
		}
		//cooldown state
		if(attacked)
		{
			timer += delta;
			if(timer >= cooldown)
			{
				attacked = false;
				timer = 0;
			}
		}
	}
}
