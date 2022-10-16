using Godot;
using System;
using System.Collections.Generic;	//Dictionary

public class AudioManager : Node
{
	/*=============================================================== Members =======================================================*/
	//private GameManager gManager;
	
	//[Export]
	private static string SOUND_PATH = "res://assets/sounds/";
	
	//[Export]
	private static string MUSIC_PATH = (SOUND_PATH + "music/");
	
	private Node currentMusicNode;
	private AudioStreamPlayer currentMusic;
	
	private Node currentSoundsNode;
	
	//Format: Dictionary<scene name, music track>
	//[Export]
	private Dictionary<string, AudioStream> loadedMusic;
	
	//Format: Dictionary<domainName, Dictionary<domain soundName, Sound stream>>
	//[Export]
	private Dictionary<string, Dictionary<string, AudioStream>> loadedSounds;
	
	//Format: Dictionary<scene name, music track path>
	//[Export]
	private Dictionary<string, string> musicPaths = 
		new Dictionary<string, string> 
		{
			{"GameOver", 			(MUSIC_PATH + "lose_screen_music.mp3")},
			{"Level1", 				(MUSIC_PATH + "main_stage_music.mp3")},
			{"BossLevel", 			(MUSIC_PATH + "boss_stage_music.mp3")},
			{"StartScreen", 		(MUSIC_PATH + "main_menu_music.mp3")},
			{"Win", 				(MUSIC_PATH + "win_screen_music.mp3")}		
		};
	
	//Format: Dictionary<domainName, Dictionary<domain soundName, Sound path>>
	//[Export]
	private Dictionary<string, Dictionary<string, string>> soundPaths = 
		new Dictionary<string, Dictionary<string, string>> 
		{
			{
				"OniBoss", 
				new Dictionary<string, string>
				{	            
					{"Damage", 			(SOUND_PATH + "enemy/enemy_oni_boss_damage.wav")},
					{"Death", 			(SOUND_PATH + "enemy/enemy_oni_boss_death.wav")},
					{"DeathVaporize", 	(SOUND_PATH + "enemy/enemy_oni_boss_death_vaporize.wav")},
					{"Drop", 			(SOUND_PATH + "enemy/enemy_oni_boss_drop.wav")},
					{"DrumAttack", 		(SOUND_PATH + "enemy/enemy_oni_boss_drum-attack.wav")},
					{"DrumCharge", 		(SOUND_PATH + "enemy/enemy_oni_boss_drum-charge.wav")},
					{"Laugh", 			(SOUND_PATH + "enemy/enemy_oni_boss_laugh.wav")},
					{"Phase2_Groan", 	(SOUND_PATH + "enemy/enemy_oni_boss_groan.wav")}
				}
			},
			{
				"OniBrute", 
				new Dictionary<string, string>
				{
					{"Damage", 			(SOUND_PATH + "enemy/enemy_oni_damage.wav")},
					{"Death", 			(SOUND_PATH + "enemy/enemy_oni_death.wav")}
				}
			},
			{
				"Player", 
				new Dictionary<string, string>
				{
					{"Attack", 			(SOUND_PATH + "player/player_attack_16bit.wav")},
					{"Damage", 			(SOUND_PATH + "player/player_damage.wav")},
					{"Death",			(SOUND_PATH + "nosound.wav")},
					{"Jump", 			(SOUND_PATH + "player/player_jump_16bit.wav")}
				}
			},
			{
				"Powerup", 
				new Dictionary<string, string>
				{
					{"Cottoncandy", 	(SOUND_PATH + "powerups/heal.wav")},
					{"Dango", 			(SOUND_PATH + "powerups/jump-boost.wav")},
					{"Onigiri", 		(SOUND_PATH + "powerups/speed-boost.wav")},
					{"Squid", 			(SOUND_PATH + "powerups/attack-boost.wav")}
				}
			},
			{
				"UserInterface", 
				new Dictionary<string, string>
				{
					{"QuitButtonPress",	(SOUND_PATH + "menu/button_press.wav")},
					{"StartButtonPress",(SOUND_PATH + "menu/button_press2.wav")}
				}
			}
		};
		
	
	/*=============================================================== Methods =======================================================*/
	
	public void LoadMusic(string trackName, bool forceLoad = false)
	{
		string musicTrackPathRef = "";
		//If the requested track is not defined in our filepaths
		if(!musicPaths.TryGetValue(trackName, out musicTrackPathRef))
		{
			//Handle errors
			return;
		}
		//If entry exists and forceLoad == true, assign value (Used for reloading music)
		if(loadedMusic.TryGetValue(trackName, out AudioStream streamRef))
		{
			streamRef = forceLoad ? ResourceLoader.Load<AudioStream>(musicTrackPathRef) : streamRef;
		}
		//If music entry doesn't exist
		else
		{
			loadedMusic.Add(trackName, ResourceLoader.Load<AudioStream>(musicTrackPathRef));
		}
	}
	
	public void LoadDomainSounds(string domainName, bool forceLoad = false)
	{
		Dictionary<string, AudioStream> domainDictRef = new Dictionary<string, AudioStream>();
		if(!loadedSounds.TryGetValue(domainName, out domainDictRef))	//If the domain doesnt exist (e,g,, Player, OniBrute, etc.)
		{
			//Handle errors
			GD.Print(domainName, " sounds aren't initialized!");
			return;
		}
		if(soundPaths == null)
		{
			//Handle errors
			return;
		}
		foreach(KeyValuePair<string, string> entry in soundPaths[domainName])
		{
			//If entry exists and forceLoad == true, assign value (Used for reloading sounds)
			if(domainDictRef.TryGetValue(entry.Key, out AudioStream streamRef))
			{
				streamRef = forceLoad ? ResourceLoader.Load<AudioStream>(entry.Value) : streamRef;
			}
			//If sound entry doesn't exist
			else
			{
				domainDictRef.Add(entry.Key, ResourceLoader.Load<AudioStream>(entry.Value));
			}
		}
	}
	
	public void PlayMusic(string musicName) 
	{
		if(currentMusic.Stream != null && currentMusic.Playing) {
			currentMusic.Stop();
		}
		currentMusic.Stream = loadedMusic[musicName];
		currentMusic.Name = musicName;
		currentMusic.Bus = "Music";
		currentMusic.Play();
	}
	
	public void PlaySound(string domainName, string soundName) 
	{
		//GD.Print("Played sound: " + soundName);
		AudioStreamPlayer soundPlayer = new AudioStreamPlayer();
		soundPlayer.Stream = loadedSounds[domainName][soundName];
		soundPlayer.Name = soundName + "Sound";
		soundPlayer.Bus = "Sounds";
		currentSoundsNode.AddChild(soundPlayer);
		soundPlayer.Connect("finished", 
							this, 
							nameof(_on_AudioStreamPlayer_finished),
							new Godot.Collections.Array() { soundPlayer }
							);
		soundPlayer.Play();
	}
	
	public void PlaySound2D(string domainName, string soundName, Node2D entity)
	{
		AudioStreamPlayer2D soundPlayer2D = new AudioStreamPlayer2D();
		soundPlayer2D.Stream = loadedSounds[domainName][soundName];
		soundPlayer2D.Name = soundName + "Sound";
		soundPlayer2D.Bus = "Sounds";
		//currentSoundsNode.AddChild(soundPlayer2D);
		soundPlayer2D.Connect("finished", 
							this, 
							nameof(_on_AudioStreamPlayer2D_finished),
							new Godot.Collections.Array() { soundPlayer2D }
							);
		entity.AddChild(soundPlayer2D);
		soundPlayer2D.Play();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AudioServer.SetBusLayout(ResourceLoader.Load<AudioBusLayout>("res://Game_Buses.tres"));
		//gManager = (GameManager)GetNode("/root/GameManager");
		//This Node will centralize all sounds; manage all current sounds (mute, stop, etc.)
		currentSoundsNode = new Node();
		currentSoundsNode.Name = "Sounds";
		this.AddChild(currentSoundsNode);
		
		//This Node will centralize all music tracks; in case we want to play multiple music tracks and organization
		currentMusicNode = new Node();
		currentMusicNode.Name = "Music";
		this.AddChild(currentMusicNode);
		
		loadedMusic = new Dictionary<string, AudioStream>();
		
		//Initialize pre-determined domains
		loadedSounds = new Dictionary<string, Dictionary<string, AudioStream>> 
		{
			{ "OniBoss", new Dictionary<string, AudioStream>{ } },
			{ "OniBrute", new Dictionary<string, AudioStream>{ } },
			{ "Player", new Dictionary<string, AudioStream>{ } },
			{ "Powerup", new Dictionary<string, AudioStream>{ } },
			{ "UserInterface", new Dictionary<string, AudioStream> { } }
		};
		
		LoadMusic("GameOver");
		LoadMusic("Level1");
		LoadMusic("BossLevel");
		LoadMusic("StartScreen");
		LoadMusic("Win");
		
		LoadDomainSounds("OniBoss");
		LoadDomainSounds("OniBrute");
		LoadDomainSounds("Player");
		LoadDomainSounds("Powerup");
		LoadDomainSounds("UserInterface");
		
		currentMusic = new AudioStreamPlayer();
		currentMusic.Stream = loadedMusic["Level1"];
		currentMusic.Name = "Level1";
		currentMusic.Bus = "Music";
		currentMusicNode.AddChild(currentMusic);
		
		SetBusVolume("Master", -6f);
		SetBusVolume("Music", -10f);
	}
	
	private void _on_AudioStreamPlayer_finished(AudioStreamPlayer soundPlayer)
	{
		soundPlayer.QueueFree();
		//GD.Print("Player", audioPlayer.Name, " freed");
	}
	
	private void _on_AudioStreamPlayer2D_finished(AudioStreamPlayer2D soundPlayer2D)
	{
		soundPlayer2D.QueueFree();
	}
	
	private void SetBusMute(string busName, bool enable)
	{
		AudioServer.SetBusMute(AudioServer.GetBusIndex(busName), enable);
	}
	
	private void SetBusVolume(string busName, float volumeDb)
	{
		AudioServer.SetBusVolumeDb(AudioServer.GetBusIndex(busName), volumeDb);
	}

}
