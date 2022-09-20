using Godot;
using System;

public class SignalManager : Node
{
	[Signal]
	public delegate void PlaySoundSignal(string entityName, string soundName);
}
