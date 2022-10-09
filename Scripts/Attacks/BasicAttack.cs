using Godot;
using System;
using System.Threading.Tasks;

public class BasicAttack : Attack
{
	//The range of the attack
	private CollisionPolygon2D range;

	protected override void GetRange()
	{    
		range = (CollisionPolygon2D)GetNode("Range");
	}

	protected override void SetColliderActive()
	{
		range.SetDeferred("disabled", !attacking);
	}

	protected override async Task ActivateCollider()
	{
		//Play the sound & animation (PlayAnimation MUST come before host.Attacking = true;)
		host.PlayAnimation(name);
		attacking = true;
		host.Attacking = true;
		host.GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
										host.GetType().Name,
										"Attack"
										);

		//Enable the attack range until animation is finished
		await ToSignal(host.Animator, "animation_finished");

		attacking = false;
		host.Attacking = false;
		host.PlayAnimation(previousAnim);
		host.FaceAttacks();

		await WaitCooldown();
	}

	protected override async Task WaitCooldown()
	{
		//Wait until timer is timed-out
		time.Start(0);
		await ToSignal(time, "timeout");
		time.Stop();

		waiting = false;
		GD.Print("Cooldown");
	}
}
