using Godot;
using System;


public class CameraController : Camera2D
{
	private GameManager gManager;
	private Node2D player;
	//private Player gManager.PlayerRef;
	private float MostLeft = 0;

	public override void _Ready()
	{
		gManager = (GameManager)GetNode("/root/GameManager");
		player = gManager.PlayerRef;
	}

	public override void _PhysicsProcess(float delta)
	{
		//Update the Camera's position when as the player moves right
		if (player.GlobalPosition.x > MostLeft + 15)
		{
			Vector2 temp = GlobalPosition;
			temp.x = player.GlobalPosition.x;
			GlobalPosition = temp;
			MostLeft = player.GlobalPosition.x - 15;
		}
	}
}
