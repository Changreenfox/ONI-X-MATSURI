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
		
		Timer newTimer = new Timer();
		newTimer.OneShot = true;
		AddChild(newTimer);
		newTimer.Start(boostTime);

		await ToSignal(newTimer, "timeout");
		
		player.DamageBoost -= damageIncrease;
		Interface?.Toggle_Powerup_Icon("Attack");

		QueueFree();
	}
}
