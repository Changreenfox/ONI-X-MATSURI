using Godot;
using System;

public class LastChunkTower : Node2D
{
	private GameManager gManager;
	
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
	}
	
	public void _on_EndWall_body_entered(object body)
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									"res://Scenes/BossLevel.tscn"
									);
	}
}
