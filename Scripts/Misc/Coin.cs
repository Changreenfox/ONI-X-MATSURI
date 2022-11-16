using Godot;
using System;

public class Coin : Sprite
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	private void PlaySound(string soundName)
	{
		AudioStreamPlayer sound = GetNodeOrNull<AudioStreamPlayer>(soundName);
		sound?.Play();
	}
}
