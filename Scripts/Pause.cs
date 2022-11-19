using Godot;
using System;

public class Pause : Control
{
	private GameManager gManager;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
	}

	private void _on_Pause_pressed()
	{
		GetTree().Paused = true;
		Show();
	}
	
	private void _on_Resume_pressed()
	{
		GetTree().Paused = false;
		Hide();
	}
	
	private void _on_Quit_pressed()
	{
		Hide();
		GetTree().Paused = false;
		gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									"res://Scenes/Start.tscn"
									);
		//GetTree().ChangeScene("res://Scenes/Start.tscn");
	}
}





