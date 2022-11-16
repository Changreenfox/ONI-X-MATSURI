using Godot;
using System;

public class CreditsScreen : SceneBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		isGameplay = false;
	}

	public void _on_any_input_pressed()
	{
		GetTree().ChangeScene("res://Scenes/Start.tscn");
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed == true) {
				_on_any_input_pressed();
			}
		}
		if (@event is InputEventMouseButton eventMouseButton) {
			_on_any_input_pressed();
		}
	}

}
