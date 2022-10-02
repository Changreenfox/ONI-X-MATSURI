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
	}

	protected override void SetColliderActive()
	{
		range.SetDeferred("disabled", !attacking);
	}

    public override void _on_Attack_body_entered(KinematicBody2D collision)
	{
        base._on_Attack_body_entered(collision);
		active = true;
	}

	protected override async Task WaitCooldown()
	{
		attacking = false;
		//Wait until timer is timed-out
		time.Start(0);
		await ToSignal(time, "timeout");
		time.Stop();

		attacking = true;
		waiting = false;
	}
}
