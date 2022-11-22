using Godot;
using System;

public class FadeTransition : CanvasLayer
{
	private GameManager gManager;
	
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
		((AnimationPlayer)GetNode("AnimationPlayer")).Play("fade_to_normal");
	}
	
	public void SceneTransition(string SceneName)
	{
		_TransitionScene(SceneName);
	}
	
	protected async void _TransitionScene(string SceneName)
	{
		((AnimationPlayer)GetNode("AnimationPlayer")).Play("fade_to_black");
		await ToSignal(GetTree().CreateTimer(0.5f), "timeout");
		gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									SceneName
									);
	}
}
