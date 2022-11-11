using Godot;
using System;

public class Death : State
{
	private AudioStreamPlayer2D deathSound;
	
	public Death(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		deathSound = host.GetNode("Sounds").GetNode<AudioStreamPlayer2D>("Death");

		host.AfterLost();
		host.CancelAttack();
		//host.SetProcess(false);
		PlayDeathSound();
		PlayAnimation();
		Process();
	}
	
	public override string StateName()
	{
		return "Death";
	}
	
	private async void Process()
	{
		await ToSignal(host.Animator, "animation_finished");
		host.Die();
	}
	
	private async void PlayDeathSound()
	{
		/*
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name,
										"Death"
										);
		*/
		deathSound.Play();
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Death");
	}
}
