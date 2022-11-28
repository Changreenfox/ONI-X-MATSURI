using Godot;
using System;

public class LastChunkTower : Node2D
{
	private FadeTransition Transition;
	//private GameManager gManager;
	
	public override void _Ready()
	{
		Transition = GetParent().GetParent().GetNode<FadeTransition>("TransitionScreen");
		//gManager = GetNode<GameManager>("/root/GameManager");
	}
	
	public void _on_EndWall_body_entered(object body)
	{
		Transition.SceneTransition("res://Scenes/BossLevel.tscn");
		//gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
		//							"res://Scenes/BossLevel.tscn"
		//							);
		//GetTree().ChangeScene("res://Scenes/Tower.tscn");
	}
}
