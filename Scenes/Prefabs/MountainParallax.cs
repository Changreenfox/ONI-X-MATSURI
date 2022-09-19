using Godot;
using System;

public class MountainParallax : Node2D
{
	private Player PlayerNode;
	private Node2D Mountains;
	private Node2D Skys;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<Player>("/root/World/Player");
		Mountains = GetNode<Node2D>("Mountains");
		
		Skys = GetNode<Node2D>("Sky");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		Vector2 newV = new Vector2(Mountains.Position.x + PlayerNode.Velocity.x * 0.005f, 0);
	 	Mountains.Position = newV;
		
		newV = new Vector2(Skys.Position.x + PlayerNode.Velocity.x * 0.008f, 0);
		Skys.Position = newV;
	}
	
	
}
