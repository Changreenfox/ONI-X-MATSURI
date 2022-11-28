/*
Class to declare, emit, and connect custom signals between classes.
*/

using Godot;
using System;

public class SignalManager : Node
{	
	[Signal]
	public delegate void EnemyDied(int scoreValue);
	
	[Signal]
	public delegate void EntityCreated(string domainName);
	
	[Signal]
	public delegate void OniBossAttacked();
	
	[Signal]
	public delegate void OniBossDied();
	
	[Signal]
	public delegate void OniBossLanded();
	
	[Signal]
	public delegate void OniBossPhase2();
	
	[Signal]
	public delegate void PlayerDied();
	
	[Signal]
	public delegate void PlayerLoaded(Player player);
	
	[Signal]
	public delegate void PlaySoundSignal(string domainName, string soundName);
	
	[Signal]
	public delegate void PlaySound2DSignal(string domainName, string soundName, Node2D entity);
	
	[Signal]
	public delegate void SceneChangeCall(string newSceneName);
	
	[Signal]
	public delegate void SceneLoadedSignal(string sceneName);
	
	[Signal]
	public delegate void UpdatedGameScore(int gameScore);
}
