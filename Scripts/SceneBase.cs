using Godot;
using System;

public class SceneBase : Node
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	protected GameManager gManager;
	public GameManager GManager
	{
		get{ return gManager; }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//SHOULD ONLY PLAY MUSIC AFTER THE SCREEN IS SHOWING (MAYBE NEED TO ADD CHECKS?) - RICARDO
		gManager = (GameManager)GetNode("/root/GameManager");
		gManager.CurrentScene = this;
		gManager.Signals.EmitSignal(
									nameof(SignalManager.SceneLoadedSignal),
									"Level1"
									);
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}