using Godot;
using System;

public class Win : SceneBase
{
	private FadeTransition Transition;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		isGameplay = false;
		Transition = GetNode<FadeTransition>("TransitionScreen");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_ReturnButton_pressed()
	{
		Transition.SceneTransition("res://Scenes/Start.tscn");
		//gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
		//							"res://Scenes/Start.tscn"
		//							);
		//GetTree().ChangeScene("res://Scenes/Start.tscn");
	}

}


