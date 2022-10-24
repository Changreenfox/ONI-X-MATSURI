using Godot;
using System;

public class Level2Generator : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int NumChunks = 9; // Total number of unique chunks (not including tutorial and end)
		int MarkerY = -1080; // Holds location information for loading the next chunk
		
		// Set up RNG
		var random = new RandomNumberGenerator();
		random.Randomize();
		
		// Load and instance Tutorial
		PackedScene Scene = GD.Load<PackedScene>("res://Scenes/Prefabs/Tilemaps/Tower/Bottom.tscn");
		Node2D Tutorial = (Node2D)Scene.Instance();
		AddChild(Tutorial);
		
		// Load Chunks
		Vector2 pos;
		for (int i = 0; i < 5; i++)
		{
			double rand = random.RandiRange(1, NumChunks);
			
			Scene = GD.Load<PackedScene>("res://Scenes/Prefabs/Tilemaps/Tower/Chunk" + rand + ".tscn");
			Node2D Chunk = (Node2D)Scene.Instance();
			AddChild(Chunk);
			
			pos = new Vector2(0, MarkerY);
			Chunk.Position = pos;
			MarkerY -= 1080;
		}
		
		// Instance End Chunk
		Scene = GD.Load<PackedScene>("res://Scenes/Prefabs/Tilemaps/Tower/Top.tscn");
		Node2D End = (Node2D)Scene.Instance();
		AddChild(End);
		pos = new Vector2(0, MarkerY);
		End.Position = pos;
	}

}
