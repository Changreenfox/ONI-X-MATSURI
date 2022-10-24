using Godot;
using System;

public class MountainParallax : Node2D
{
	private Player PlayerNode;
	private Camera2D CameraNode;
	private Node2D Mountains;
	private Node2D Skys;
	
	private Vector2 PrevCamPos;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		CameraNode = GetNode<Camera2D>("/root/World/Camera2D");
		PlayerNode = GetNode<Player>("/root/World/Player");
		Mountains = GetNode<Node2D>("Mountains");
		Skys = GetNode<Node2D>("Sky");
		
		PrevCamPos = CameraNode.GetCameraScreenCenter();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Vector2 CurrCamPos = CameraNode.GetCameraScreenCenter();
		float CamVelocityX = CurrCamPos.x - PrevCamPos.x;
		
		Vector2 newV = new Vector2(Mountains.Position.x + CamVelocityX * 0.5f, 0);
	 	Mountains.Position = newV;
		
		newV = new Vector2(Skys.Position.x + CamVelocityX * 0.8f, 0);
		Skys.Position = newV;
		
		PrevCamPos = CameraNode.GetCameraScreenCenter();
	}
	
	
}
