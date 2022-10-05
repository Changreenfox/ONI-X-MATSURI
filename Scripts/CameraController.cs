using Godot;
using System;



public class CameraController : Camera2D
{
	GameManager gManager;
	private Player PlayerNode;
	private float MostLeft = 0;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = (GameManager)GetNode("/root/GameManager");
		PlayerNode = gManager.PlayerRef;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		//Update the Camera's position when as the player moves right
		if (PlayerNode.GlobalPosition.x > MostLeft + 15)
		{
			Vector2 temp = GlobalPosition;
			temp.x = PlayerNode.GlobalPosition.x;
			GlobalPosition = temp;
			MostLeft = PlayerNode.GlobalPosition.x - 15;
		}
	}
}
