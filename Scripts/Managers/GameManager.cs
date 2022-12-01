/*
References:
	https://www.youtube.com/watch?v=yLbqimzD94A ("Godot Beginner Tutorial 12: Singleton" | Alvin Roe)
*/
using Godot;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GameManager : Node
{
	private PlayerData playerData = null;
	
	private Player playerRef;
	public Player PlayerRef
	{
		get{ return playerRef; }
		set{ playerRef = value; }
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
		set
		{ 
			currentScene = value; 
			signals.EmitSignal(nameof(SignalManager.UpdatedGameScore),
								gameScore
								);
		}
	}
	
	private Camera2D currentCamera;
	public Camera2D CurrentCamera
	{
		get{ return currentCamera; }
		set{ currentCamera = value; }
	}
	
	[Export]
	private int gameScore = 0;
	public int GameScore
	{
		get{ return gameScore; }
	}
	
	[Export]
	private float playTime = 0;
	public float PlayTime
	{
		get { return playTime; }
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		audio = (AudioManager)GetNode("/root/AudioManager");
		signals = (SignalManager)GetNode("/root/SignalManager");
		
		signals.Connect(nameof(SignalManager.SceneLoadedSignal), audio, nameof(AudioManager.PlayMusic));
		signals.Connect(nameof(SignalManager.PlaySoundSignal), audio, nameof(AudioManager.PlaySound));
		signals.Connect(nameof(SignalManager.PlaySound2DSignal), audio, nameof(AudioManager.PlaySound2D));
		signals.Connect(nameof(SignalManager.EnemyDied), this, nameof(this.SetGameScore));
		
		signals.Connect(nameof(SignalManager.PlayerDied), this, nameof(this.HandlePlayerDeath));
		signals.Connect(nameof(SignalManager.GameReset), this, nameof(this.ResetGame));
		
		signals.Connect(nameof(SignalManager.UIManagerLoaded), this, nameof(this.LoadUIManager));
		signals.Connect(nameof(SignalManager.PlayerLoaded), this, nameof(this.LoadPlayer));
		signals.Connect(nameof(SignalManager.SceneChangeCall), this, nameof(this.HandleSceneChange));
	}
	
	public override void _Process(float delta)
	{
		if(currentScene.IsGameplay)
		{
			playTime += delta;
		}
	}
	
	
	private void SetGameScore(int scoreValue)
	{
		PlayerCoinGet(scoreValue);
	}
	
	private async void PlayerCoinGet(int scoreValue)
	{
		PackedScene coinScene = ResourceLoader.Load<PackedScene>($"res://Scenes/Prefabs/Coin.tscn");
		Timer delayTimer = new Timer();
		delayTimer.Name = "CoinTimer";
		AddChild(delayTimer);
		for(int i = 0; i < scoreValue; ++i)
		{
			Sprite coin = coinScene.Instance() as Sprite;
			coin.Position = playerRef.Position;
			CallDeferred("add_child", coin);	//(DOESNT WORK) Add as child of Player so the sprite moves with the Player
			AnimationPlayer coinAnimations = coin.GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
			coinAnimations?.Play("PlayerGet");	//Handles freeing the node
			
			gameScore += 1;
			signals.EmitSignal(nameof(SignalManager.UpdatedGameScore),
							gameScore
							);
							
			delayTimer.Start(0.2f);
			await ToSignal(delayTimer, "timeout");
		}
		delayTimer.QueueFree();
	}
	
	private void HandlePlayerDeath()
	{
		HandleSceneChange("res://Scenes/GameOver.tscn");
		playerRef = null;
		playerData = null;
	}
	
	private void HandleSceneChange(string newSceneName)
	{
		if(playerRef != null)
		{
			playerData = new PlayerData(playerRef);
		}
		else
		{
			playerData = null;
		}
		
		if(!currentScene.IsGameplay)
		{
			playerRef = null;
			playerData = null;
		}
		
		//Handle scene transitions
		if(currentScene != null)
		{
			currentScene.GetTree().ChangeScene(newSceneName);
		}
	}
	
	private void LoadUIManager()
	{
		GD.Print("Gamescore: ", gameScore);
		//Called at every new Scene entry to update the display to the current game score
		signals.EmitSignal(nameof(SignalManager.UpdatedGameScore),
							gameScore
							);
	}
	
	private void LoadPlayer(Player player)
	{
		playerRef = player;
		if(playerData != null)
		{
			playerRef.HP = playerData.HealthPoints;
		}
	}
	
	private void ResetGame()
	{
		playerRef = null;
		playerData = null;
		gameScore = 0;
		playTime = 0;
		GD.Print("Gamescore updated to: ", gameScore);
	}
}
