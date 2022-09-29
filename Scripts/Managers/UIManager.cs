using Godot;
using System;

public class UIManager : Control
{
	private Player PlayerNode;
	private TextureRect Heart1;
	private TextureRect Heart2;
	private TextureRect Heart3;
	
	private Texture HeartFull;
	private Texture HeartHalf;
	private Texture HeartEmpty;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		PlayerNode = GetNode<Player>("/root/World/Player");
		Heart1 = GetNode<TextureRect>("Left/Heart1");
		Heart2 = GetNode<TextureRect>("Left/Heart2");
		Heart3 = GetNode<TextureRect>("Left/Heart3");
		
		HeartFull = (Texture)GD.Load("res://assets/sprites/heart_full.png");
		HeartHalf = (Texture)GD.Load("res://assets/sprites/heart_half.png");
		HeartEmpty = (Texture)GD.Load("res://assets/sprites/heart_empty.png");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// Set heart sprites based on player health
		if (PlayerNode.HP <= 2)
		{
			if(PlayerNode.HP == 1)
				Heart1.Texture = HeartHalf;
			else
				Heart1.Texture = HeartFull;
			Heart2.Texture = HeartEmpty;
			Heart3.Texture = HeartEmpty;
		}
		else if (PlayerNode.HP <= 4)
		{
			Heart1.Texture = HeartFull;
			if(PlayerNode.HP == 3)
				Heart2.Texture = HeartHalf;
			else
				Heart2.Texture = HeartFull;
			Heart3.Texture = HeartEmpty;
		}
		else
		{
			Heart1.Texture = HeartFull;
			Heart2.Texture = HeartFull;
			if(PlayerNode.HP == 5)
				Heart3.Texture = HeartHalf;
			else
				Heart3.Texture = HeartFull;
		}
	}

	private void _on_Exit_pressed()
	{
		GetTree().Quit();
	}
}


