using Godot;
using System;
using System.Threading.Tasks;

public class ContactDamage : Attack
{
	//The range of the attack
	private CollisionShape2D range;

	public override void _Ready()
	{
		base._Ready();
		SetProcess(false);
	}

	protected override void GetRange()
	{    
		range = (CollisionShape2D)GetNode("Range");
	}

	//Detect when an enemy is within range
	public override void _on_Attack_body_entered(KinematicBody2D collision)
	{
		base._on_Attack_body_entered(collision);
		//range.SetDeferred("disabled", true);
	}
	
	public void EnableCollider()
	{
		range.SetDeferred("disabled", false);
	}
}
