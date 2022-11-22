using Godot;
using System;

public class FadeTransition : CanvasLayer
{
	public override void _Ready()
	{
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
		GetTree().ChangeScene(SceneName);
	}
}
