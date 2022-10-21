using Godot;
using System;

public class SpeedPowerUp : Powerup
{
	[Export]
	private float speedIncrease = 400;
	
	protected async override void ActivatePowerUp()
	{
		player.MaxSpeed += speedIncrease;
		Interface?.Toggle_Powerup_Icon("Speed");
		
		await ToSignal(GetTree().CreateTimer(boostTime), "timeout");
		
		player.MaxSpeed -= speedIncrease;
		Interface?.Toggle_Powerup_Icon("Speed");

		parent.QueueFree();
	}
}
