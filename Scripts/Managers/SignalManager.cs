/*
Class to declare, emit, and connect custom signals between classes.
*/

using Godot;
using System;

public class SignalManager : Node
{	
	[Signal]
	public delegate void EntityCreated(string domainName);
	
	[Signal]
	public delegate void OniBossAttacked();
	
	[Signal]
	public delegate void OniBossLanded();
	
	[Signal]
	public delegate void OniBossPhase2();
	
	[Signal]
	public delegate void PlaySoundSignal(string domainName, string soundName);
	
	[Signal]
	public delegate void PlaySound2DSignal(string domainName, string soundName, Node2D entity);
	
	[Signal]
	public delegate void SceneLoadedSignal(string sceneName);
}
