using Godot;
using System;

public class LastChunk : Node2D
{
	//private GameManager gManager;
	private FadeTransition Transition;
	
	public override void _Ready()
	{
		Transition = GetParent().GetParent().GetNode<FadeTransition>("TransitionScreen");
		//gManager = GetNode<GameManager>("/root/GameManager");
	}
	
	public void _on_EndWall_body_entered(object body)
	{
		Transition.SceneTransition("res://Scenes/Tower.tscn");
		//gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
		//							"res://Scenes/Tower.tscn"
		//							);
		//GetTree().ChangeScene("res://Scenes/Tower.tscn");
	}
}
