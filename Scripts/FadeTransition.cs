using Godot;
using System;

public class FadeTransition : CanvasLayer
{
	private GameManager gManager;
	private Timer timer;
	
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
		timer = new Timer();
		AddChild(timer);
		((AnimationPlayer)GetNode("AnimationPlayer")).Play("fade_to_normal");
	}
	
	public void SceneTransition(string SceneName)
	{
		_TransitionScene(SceneName);
	}
	
	protected async void _TransitionScene(string SceneName)
	{
		((AnimationPlayer)GetNode("AnimationPlayer")).Play("fade_to_black");
		timer.Start(0.5f);
		await ToSignal(timer, "timeout");
		gManager.Signals.EmitSignal(nameof(SignalManager.SceneChangeCall),
									SceneName
									);
	}
}
