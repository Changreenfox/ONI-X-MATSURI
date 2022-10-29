using Godot;
using System;

public class AttackPowerUp : PowerUp
{
	[Export]
	private int damageIncrease = 1;
	
	protected async override void ActivatePowerUp()
	{
		player.DamageBoost += damageIncrease;
		Interface?.Toggle_Powerup_Icon("Attack");
		
		await ToSignal(GetTree().CreateTimer(boostTime), "timeout");
		
		player.DamageBoost -= damageIncrease;
		Interface?.Toggle_Powerup_Icon("Attack");

		QueueFree();
	}
}
