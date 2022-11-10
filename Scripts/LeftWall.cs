using Godot;
using System;

public class LeftWall : CollisionShape2D
{
	[Export]
	private float limit = 0f;

	public override void _PhysicsProcess(float delta)
	{
		//Update the Camera's position when as the player moves right
		if (GlobalPosition.x > limit)
		{
			Vector2 temp = GlobalPosition;
			temp.x = limit;
			GlobalPosition = temp;
		}
	}
}
