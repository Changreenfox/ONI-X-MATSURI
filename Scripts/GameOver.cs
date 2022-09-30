using Godot;
using System;

public class GameOver : SceneBase
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		gManager.Audio.PlaySound("OniBoss", "Laugh");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private void _on_ReturnButton_pressed()
	{
		GetTree().ChangeScene("res://Scenes/Start.tscn");
	}

}


