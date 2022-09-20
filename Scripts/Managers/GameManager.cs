/*
References:
	https://www.youtube.com/watch?v=yLbqimzD94A ("Godot Beginner Tutorial 12: Singleton" | Alvin Roe)
*/
using Godot;
using System;

public class GameManager : Node
{
	private AudioManager audio;
	public AudioManager Audio
	{
		get{ return audio; }
	}
	
	private SignalManager signals;
	public SignalManager Signals
	{
		get{ return signals; }
	}
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	/*
	private CustomSignals signals;
	public CustomSignals Signals
	{
		get{ return signals; }
	}
	*/

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = (AudioManager)GetNode("/root/AudioManager");
		signals = (SignalManager)GetNode("/root/SignalManager");
		
		signals.Connect(nameof(SignalManager.PlaySoundSignal), audio, nameof(AudioManager.PlaySound));
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
