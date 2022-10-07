using Godot;
using System;

public class Powerup : Node2D
{
	[Signal] public delegate PowerupPicked(int type);
	protected Sprite Parent;
	protected float yPos;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Parent = GetParent();
		yPos = Position.y;
	}

 	public override void _Process(float delta)
	{
		
	}
	
	private void _on_Area2D_body_entered(object body)
	{
		EmitSignal("PowerupPicked", 0);
		Parent.Hide();
	}
}





