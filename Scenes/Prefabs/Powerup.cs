using Godot;
using System;

public class Powerup : Node2D
{
	private Player PlayerNode;
	protected Sprite Parent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<Player>("/root/World/Player");
		Parent = (Sprite)GetParent();
	}

 	//public override void _Process(float delta)
	//{
		
	//}
	
	private void _on_Area2D_body_entered(object body)
	{
		PlayerNode._on_Powerup_pickup(0);
		Parent.Hide();
		GD.Print("hi");
	}
	
}





