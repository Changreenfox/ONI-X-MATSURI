using Godot;
using System;
using System.Threading.Tasks;

public class SceneBase : Node2D
{
	protected GameManager gManager;
	
	protected Camera2D currentCamera;
	public Camera2D CurrentCamera
	{
		get{ return currentCamera; }
		set{ currentCamera = value; }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//SHOULD ONLY PLAY MUSIC AFTER THE SCREEN IS SHOWING (MAYBE NEED TO ADD CHECKS?) - RICARDO
		gManager = (GameManager)GetNode("/root/GameManager");
		gManager.CurrentScene = this;
		gManager.Signals.EmitSignal(
									nameof(SignalManager.SceneLoadedSignal),
									GetType().Name
									);		
	}	
}
