using Godot;
using System;

public class HealthPowerUp : Powerup
{
	protected async override void ActivatePowerUp()
	{
		if (player.HP < player.MaxHealth)
				player.HP += 1;

		QueueFree();
	}
}
