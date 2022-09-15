using Godot;
using System;

public class BasicAttack:Area2D
{
	// Attack functionality shared by all attacks
	[Export]
	public int damage = 1;
	//Every class has the Attack function
	
	private CollisionPolygon2D range;
	
	public override void _Ready()
	{
		range = (CollisionPolygon2D)GetNode("Range");
		range.Disabled = true;
	}
	
	public void Attack()
	{
		range.Disabled = false;
		//Check if the attached collider is colliding with anything
		GD.Print("Attack");
		range.Disabled = true;
	}
}
