using Godot;
using System;

public class CustomSignals : Node
{
	[Signal]
	public delegate void PlaySoundSignal(string entityName, string soundName);
	
	public static int x = 5;
	
	public override void _Ready()
	{
		
	}
}
