using Godot;
using System;

public class Level1Generator : Node2D
{
	public override void _Ready()
	{
		int NumChunks = 5; // Total number of unique chunks (not including tutorial and end)
		int MarkerX = 1920; // Holds location information for loading the next chunk
		
		// Set up RNG
		var random = new RandomNumberGenerator();
		random.Randomize();
		
		// Load and instance Tutorial
		PackedScene Scene = GD.Load<PackedScene>("res://Scenes/Prefabs/Tilemaps/World/TutorialChunk.tscn");
		Node2D Tutorial = (Node2D)Scene.Instance();
		AddChild(Tutorial);
		
		// Load Chunks
		Vector2 pos;
		for (int i = 0; i < 7; i++)
		{
			double rand = random.RandiRange(1, NumChunks);
			
			Scene = GD.Load<PackedScene>("res://Scenes/Prefabs/Tilemaps/World/Chunk" + rand + ".tscn");
			Node2D Chunk = (Node2D)Scene.Instance();
			AddChild(Chunk);
			
			pos = new Vector2(MarkerX, 56);
			Chunk.Position = pos;
			MarkerX += (1920 * 2);
		}
		
		// Instance End Chunk
		Scene = GD.Load<PackedScene>("res://Scenes/Prefabs/Tilemaps/World/LastChunk.tscn");
		Node2D End = (Node2D)Scene.Instance();
		AddChild(End);
		pos = new Vector2(MarkerX, 56);
		End.Position = pos;
	}
}
