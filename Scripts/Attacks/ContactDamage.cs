using Godot;
using System;
using System.Threading.Tasks;

public class ContactDamage : Attack
{
	//The range of the attack
	private CollisionShape2D range;

	protected override void GetRange()
	{    
		range = (CollisionShape2D)GetNode("Range");
		attacking = true;
	}

	public override void _on_Attack_body_entered(KinematicBody2D collision)
	{
		base._on_Attack_body_entered(collision);
		active = true;
	}

	protected override async Task WaitCooldown()
	{
		range.SetDeferred("disabled", true);
		//Wait until timer is timed-out
		time.Start(cooldown);
		await ToSignal(time, "timeout");
		time.Stop();
		range.SetDeferred("disabled", false);
		waiting = false;
	}
}
