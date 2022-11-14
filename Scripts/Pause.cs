using Godot;
using System;

public class Pause : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
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
		GetTree().Quit();
		//GetTree().ChangeScene("res://Scenes/Start.tscn");
	}
}





