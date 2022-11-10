using Godot;
using System;

public class Death : State
{
	public Death(Actor _host)
	{
		host = _host;
	}

	public override void Enter()
	{
		//host.PlayAnimation("Death");
		host.AfterLost();
		host.CancelAttack();
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
		host.SetProcess(false);
		await ToSignal(host.Animator, "animation_finished");
		host.Die();
	}
	
	private async void PlayDeathSound()
	{
		/*
		This is to remove any sort of collisions or behavior with the Actor
		Sounds are played at the 2D position of the Actor
		Allows the Death sound to finish playing before deleting the Actor's node
		*/
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name,
										"Death"
										);
		//await ToSignal(host.GetNode("DeathSound"), "finished");
	}
	
	public override void PlayAnimation()
	{
		host.PlayAnimation("Death");
	}
}
