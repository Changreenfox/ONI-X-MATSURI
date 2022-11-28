using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Death : State
{
	private AudioStreamPlayer2D deathSound;
	
	public Death(Actor _host)
	{
		host = _host;
		deathSound = host.GetNode("Sounds").GetNodeOrNull<AudioStreamPlayer2D>("Death");
	}

	public override void Enter()
	{
		host.Velocity = Vector2.Zero;
		host.Disable();

		//host.SetProcess(false);
		Process();
	}

	//Stop checking for death
	public override string HandlePhysics(float delta)
	{
		return null;
	}

	//Stop checking for death
	public override string HandleProcess(float delta)
	{
		return null;
	}
	
	public override string StateName()
	{
		return "Death";
	}
	
	private async void Process()
	{
		Task Task1 = PlayDeathAnimation();
		Task Task2 = PlayDeathSound();
		var allTasks = new Task[2] {Task1, Task2};
		await Task.WhenAll(allTasks);
		host.Die();
	}
	
	private async Task PlayDeathAnimation()
	{
		PlayAnimation();
		await ToSignal(host.Animator, "animation_finished");
	}

	private async Task PlayDeathSound()
	{
		/*
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name,
										"Death"
										);
		*/
		deathSound?.Play();
		if(deathSound is null)
			return;
		await ToSignal(deathSound, "finished");
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Death");
	}
}
