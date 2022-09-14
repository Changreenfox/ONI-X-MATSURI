using Godot;
using System;

public class BasicAttack:Area2D
{
	// Attack functionality shared by all attacks
	[Export]
	public int damage = 1;
	//Every class has the Attack function
	
	private CollisionPolygon2D range;
	
	public override void Ready()
	{
		range = (CollisionPolygon2D)GetNode("Range");
		range.Disable = true;
	}
	
	public void Attack()
	{
		range.Disable = false;
		//Check if the attached collider is colliding with anything
		GD.Print("Attack");
		range.Disable = true;
	}
}
