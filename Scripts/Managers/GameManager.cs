/*
References:
	https://www.youtube.com/watch?v=yLbqimzD94A ("Godot Beginner Tutorial 12: Singleton" | Alvin Roe)
*/
using Godot;
using System;

public class GameManager : Node
{
	private Player playerRef;
	public Player PlayerRef
	{
		get{ return playerRef; }
		set{ playerRef = value; }
	}
	
	private int health;
	public int Health
	{
		get{ return health; }
		set{ health = value; }
	}
	
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
	
	private TextureManager textures;
	public TextureManager Textures
	{
		get{ return textures; }
	}
	
	private UIManager interfaceRef;
	public UIManager InterfaceRef
	{
		get{ return interfaceRef; }
		set{ interfaceRef = value; }
	}
	
	private SceneBase currentScene;
	public SceneBase CurrentScene
	{
		get{ return currentScene; }
		set{ currentScene = value; }
	}
	
	private Camera2D currentCamera;
	public Camera2D CurrentCamera
	{
		get{ return currentCamera; }
		set{ currentCamera = value; }
	}
	
	private int gameScore = 0;
	public int GameScore
	{
		get{ return gameScore; }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = (AudioManager)GetNode("/root/AudioManager");
		signals = (SignalManager)GetNode("/root/SignalManager");
		textures = (TextureManager)GetNode("/root/TextureManager");
		
		signals.Connect(nameof(SignalManager.SceneLoadedSignal), audio, nameof(AudioManager.PlayMusic));
		signals.Connect(nameof(SignalManager.PlaySoundSignal), audio, nameof(AudioManager.PlaySound));
		signals.Connect(nameof(SignalManager.PlaySound2DSignal), audio, nameof(AudioManager.PlaySound2D));
		signals.Connect(nameof(SignalManager.EnemyDied), this, nameof(this.SetGameScore));
	}
	
	
	private void SetGameScore(int scoreValue)
	{
		gameScore += scoreValue;
		GD.Print(gameScore);
		//Update UI score
	}

}
