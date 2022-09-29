using Godot;
using System;

public class ContactDamage : Attack
{
    //The range of the attack
	private CollisionShape2D range;

	public override void GetRange()
	{    
        range = (CollisionShape2D)GetNode("Range");
	}

    public override void _Ready()
    {
        base._Ready();
        cooldown = 0.5f;
    }

    //Update state of attack
	public override void _Process(float delta)
	{
		//Use this function to be collider-safe
		range.SetDeferred("disabled", attacked);

		//cooldown state
		if(attacked)
		{
			timer += delta;
			if(timer >= cooldown)
			{
				attacked = false;
				timer = 0;
			}
		}
	}

    public override void _on_Attack_body_entered(KinematicBody2D collision)
	{
        base._on_Attack_body_entered(collision);
		attacked = true;
	}
}
