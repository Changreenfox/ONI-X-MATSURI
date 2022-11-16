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
	
	protected bool isGameplay = false;
	public bool IsGameplay
	{
		get { return isGameplay; }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = GetNodeOrNull<GameManager>("/root/GameManager");
		gManager.CurrentScene = this;
		/*gManager.Signals.EmitSignal(
									nameof(SignalManager.SceneLoadedSignal),
									GetType().Name
									);*/
	}	
}
