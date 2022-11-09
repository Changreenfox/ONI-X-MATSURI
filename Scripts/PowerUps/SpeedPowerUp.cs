using Godot;
using System;

public class SpeedPowerUp : PowerUp
{
	[Export]
	private float speedIncrease = 400;
	
	protected async override void ActivatePowerUp()
	{
		player.MaxSpeed += speedIncrease;
		Interface?.Toggle_Powerup_Icon("Speed");
		
		Timer newTimer = new Timer();
		newTimer.OneShot = true;
		AddChild(newTimer);
		newTimer.Start(boostTime);

		AudioStreamPlayer sound = GetNode<AudioStreamPlayer>("ActivationSound");
		sound.Play();

		await ToSignal(newTimer, "timeout");
		
		player.MaxSpeed -= speedIncrease;
		Interface?.Toggle_Powerup_Icon("Speed");

		QueueFree();
	}
}
