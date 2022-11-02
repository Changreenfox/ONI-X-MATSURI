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
		
		Timer newTimer = new Timer();
		newTimer.OneShot = true;
		AddChild(newTimer);
		newTimer.Start(boostTime);

		await ToSignal(newTimer, "timeout");

		player.JumpSpeed -= jumpIncrease;
		Interface?.Toggle_Powerup_Icon("Jump");

		QueueFree();
	}
}
