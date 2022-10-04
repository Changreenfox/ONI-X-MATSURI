using Godot;
using System;



public class CameraController : Camera2D
{
	private Player PlayerNode;
	private float MostLeft = 0;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<Player>("/root/World/Player");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		//Update the Camera's position when as the player moves right
		if (PlayerNode.Position.x > MostLeft + 15)
		{
			Vector2 temp = Position;
			temp.x = PlayerNode.Position.x;
			Position = temp;
			MostLeft = PlayerNode.Position.x - 15;
		}
	}
}
