using Godot;
using System;

public class Powerup : Node2D
{
	protected Player PlayerNode;
	protected Node Parent;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<Player>("/root/World/Player");
		Parent = GetParent();
	}	
	
	private void _on_Area2D_body_entered(object body)
	{
		if (Name == "Cottoncandy")
			PlayerNode._on_Powerup_pickup(1);
		else if (Name == "Dango")
			PlayerNode._on_Powerup_pickup(2);
		else if (Name == "Onigiri")
			PlayerNode._on_Powerup_pickup(3);
		else if (Name == "Squid")
			PlayerNode._on_Powerup_pickup(4);
		else
			return;
		
		Parent.QueueFree();
	}
}
