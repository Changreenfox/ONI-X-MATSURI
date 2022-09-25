using Godot;
using System;
using System.Collections.Generic;	//Dictionary

public class AudioManager : Node
{
	/*=============================================================== Members =======================================================*/
	//private GameManager gManager;
	
	private static float SOUND_VOLUME_DB = -20f;
	
	//[Export]
	private static string SOUND_PATH = "res://assets/sounds/";
		
		
	private static float MUSIC_VOLUME_DB = -20f;
	
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
			{"Defeat", 				(MUSIC_PATH + "lose_screen_music.mp3")},
			{"Level1", 				(MUSIC_PATH + "main_stage_music.mp3")},
			{"MainMenu", 			(MUSIC_PATH + "main_menu_music.mp3")},
			{"OniBoss", 			(MUSIC_PATH + "boss_stage_music.mp3")},
			{"Victory", 			(MUSIC_PATH + "win_screen_music.mp3")}		
		};
	
	//Format: Dictionary<domainName, Dictionary<domain soundName, Sound path>>
	//[Export]
	private Dictionary<string, Dictionary<string, string>> soundPaths = 
		new Dictionary<string, Dictionary<string, string>> 
		{
			{
				"enemy_oni", 
				new Dictionary<string, string>
				{
					{"damage", 			(SOUND_PATH + "enemy/enemy_oni_damage.wav")},
					{"death", 			(SOUND_PATH + "enemy/enemy_oni_death.wav")}
				}
			},
			{
				"enemy_oni_boss", 
				new Dictionary<string, string>
				{	            
					{"damage", 			(SOUND_PATH + "enemy/enemy_oni_boss_damage.wav")},
					{"death", 			(SOUND_PATH + "enemy/enemy_oni_boss_death.wav")},
					{"death_vaporize", 	(SOUND_PATH + "enemy/enemy_oni_boss_death_vaporize.wav")},
					{"drop", 			(SOUND_PATH + "enemy/enemy_oni_boss_drop.wav")},
					{"drum-attack", 	(SOUND_PATH + "enemy/enemy_oni_boss_drum-attack.wav")},
					{"drum-charge", 	(SOUND_PATH + "enemy/enemy_oni_boss_drum-charge.wav")},
					{"laugh", 			(SOUND_PATH + "enemy/enemy_oni_boss_laugh.wav")},
					{"phase2-groan", 	(SOUND_PATH + "enemy/enemy_oni_boss_groan.wav")}
				}
			},
			{
				"player", 
				new Dictionary<string, string>
				{
					{"attack", 			(SOUND_PATH + "player/player_attack_16bit.wav")},
					{"damage", 			(SOUND_PATH + "player/player_damage.wav")},
					{"jump", 			(SOUND_PATH + "player/player_jump_16bit.wav")}
				}
			},
			{
				"powerups", 
				new Dictionary<string, string>
				{
					{"attack-boost", 	(SOUND_PATH + "powerups/attack-boost.wav")},
					{"heal", 			(SOUND_PATH + "powerups/heal.wav")},
					{"jump-boost", 		(SOUND_PATH + "powerups/jump-boost.wav")},
					{"speed-boost", 	(SOUND_PATH + "powerups/speed-boost.wav")}
				}
			},
			{
				"user_interface", 
				new Dictionary<string, string>
				{
					{"quit_button_press",	(SOUND_PATH + "menu/button_press.wav")},
					{"start_button_press",	(SOUND_PATH + "menu/button_press2.wav")}
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
			//If entry doesn't exist
			else
			{
				domainDictRef.Add(entry.Key, ResourceLoader.Load<AudioStream>(entry.Value));
			}
		}
	}
	
	public void PlayMusic(string musicName) 
	{
		if(currentMusic.Stream == null && currentMusic.Playing) {
			currentMusic.Stop();
		}
		currentMusic.Stream = loadedMusic[musicName];
		currentMusic.Name = musicName;
		currentMusic.Play();
	}
	
	public void PlaySound(string domainName, string soundName) 
	{
		//GD.Print("Played sound: " + soundName);
		AudioStreamPlayer soundPlayer = new AudioStreamPlayer();
		soundPlayer.Stream = loadedSounds[domainName][soundName];
		soundPlayer.Name = soundName;
		soundPlayer.VolumeDb = SOUND_VOLUME_DB;
		currentSoundsNode.AddChild(soundPlayer);
		soundPlayer.Connect("finished", 
							this, 
							nameof(_on_AudioStreamPlayer_finished),
							new Godot.Collections.Array() { soundPlayer }
							);
		soundPlayer.Play();
	}
	
	public void SetMusicVolume(float volumeDB)
	{
		MUSIC_VOLUME_DB = volumeDB;
		foreach(AudioStreamPlayer track in currentMusicNode.GetChildren())
		{
			track.VolumeDb = volumeDB;
		}
	}
	
	public void SetSoundVolume(float volumeDB)
	{
		SOUND_VOLUME_DB = volumeDB;
		foreach(AudioStreamPlayer sound in currentSoundsNode.GetChildren())
		{
			sound.VolumeDb = volumeDB;
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
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
			{ "enemy_oni", new Dictionary<string, AudioStream>{ } },
			{ "enemy_oni_boss", new Dictionary<string, AudioStream>{ } },
			{ "player", new Dictionary<string, AudioStream>{ } },
			{ "powerups", new Dictionary<string, AudioStream>{ } },
			{ "user_interface", new Dictionary<string, AudioStream> { } }
		};
		
		LoadMusic("Level1");
		//LoadMusic("MainMenu");
		
		LoadDomainSounds("enemy_oni");
		LoadDomainSounds("enemy_oni_boss");
		LoadDomainSounds("player");
		LoadDomainSounds("powerups");
		LoadDomainSounds("user_interface");
		
		currentMusic = new AudioStreamPlayer();
		currentMusic.Stream = loadedMusic["Level1"];
		currentMusic.Name = "Level1";
		currentMusic.VolumeDb = MUSIC_VOLUME_DB;
		currentMusicNode.AddChild(currentMusic);
	}
	
	private void _on_AudioStreamPlayer_finished(AudioStreamPlayer soundPlayer)
	{
		soundPlayer.QueueFree();
		//GD.Print("Player freed");
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
