/*
Class to declare, emit, and connect custom signals between classes.
*/

using Godot;
using System;

public class SignalManager : Node
{
	[Signal]
	public delegate void SceneLoadedSignal(string sceneName);
	[Signal]
	public delegate void PlaySoundSignal(string entityName, string soundName);
}
