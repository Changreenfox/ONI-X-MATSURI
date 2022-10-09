using Godot;
using System;

public class LeftWall : CollisionShape2D
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	public override void _Process(float delta)
		{
			//Update the Camera's position when as the player moves right
			if (GlobalPosition.x > 28750)
			{
				Vector2 temp = GlobalPosition;
				temp.x = 28750;
				GlobalPosition = temp;
			}
		}
}
