using Godot;
using System;

public class JumpPowerUp : PowerUp
{
	[Export]
	private float jumpIncrease = 500;

	protected async override void ActivatePowerUp()
	{
		player.JumpSpeed += jumpIncrease;
		Interface?.Toggle_Powerup_Icon("Jump");
		
		await ToSignal(GetTree().CreateTimer(boostTime), "timeout");

		player.JumpSpeed -= jumpIncrease;
		Interface?.Toggle_Powerup_Icon("Jump");

		QueueFree();
	}
}
