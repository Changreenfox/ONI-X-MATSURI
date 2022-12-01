using Godot;
using System;
using System.Threading.Tasks;

public class FadeTransition : CanvasLayer
{
	private GameManager gManager;
	private AnimationPlayer animator;
	private Timer timer;
	
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
		animator = GetNode<AnimationPlayer>("AnimationPlayer");
		timer = GetNode<Timer>("Timer");
	}
	
	/*
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
	*/
	
	public async Task EnterScene(string newScene)
	{
		animator.Play("fade_to_normal");
		timer.Start(0.5f);
		await ToSignal(timer, "timeout");
	}
	
	public async Task ExitScene()
	{
		animator.Play("fade_to_black");
		timer.Start(0.5f);
		await ToSignal(timer, "timeout");
	}
}
