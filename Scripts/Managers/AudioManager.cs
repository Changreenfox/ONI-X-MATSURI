using Godot;
using System;
using System.Collections.Generic;	//Dictionary

public class AudioManager : Node
{
	//private GameManager gManager;
	
	private static float SOUND_VOLUME_DB = -20f;
	
	//[Export]
	private static string SOUND_PATH = "res://assets/sounds/";
		
		
	private static float MUSIC_VOLUME_DB = -20f;
	
	//[Export]
	private static string MUSIC_PATH = (SOUND_PATH + "music/");
	
	private AudioStreamPlayer currentMusic;
	
	//Format: Dictionary<scene name, music path>
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
	
	//Format: Dictionary<Entity name, Dictionary<Entity sound name, Sound path>>
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
	//Format: Dictionary<scene name, music>
	//[Export]
	private Dictionary<string, AudioStream> loadedMusic;
	
	//Format: Dictionary<Entity name, Dictionary<Entity sound name, Sound>>
	//[Export]
	private Dictionary<string, Dictionary<string, AudioStream>> loadedSounds;
	
	
	public void PlayMusic(string musicName) 
	{
		currentMusic.Stop();
		currentMusic.Stream = loadedMusic[musicName];
		currentMusic.Play();
	}
	
	public void PlaySound(string entityName, string soundName) 
	{
		//GD.Print("Played sound: " + soundName);
		AudioStreamPlayer soundPlayer = new AudioStreamPlayer();
		soundPlayer.Stream = loadedSounds[entityName][soundName];
		soundPlayer.VolumeDb = SOUND_VOLUME_DB;
		this.AddChild(soundPlayer);
		soundPlayer.Connect("finished", 
							this, 
							nameof(_on_AudioStreamPlayer_finished),
							new Godot.Collections.Array() { soundPlayer }
							);
		soundPlayer.Play();
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//gManager = (GameManager)GetNode("/root/GameManager");
		
		loadedMusic = new Dictionary<string, AudioStream>
		{
			{"Defeat", 				ResourceLoader.Load<AudioStream>(MUSIC_PATH + "lose_screen_music.mp3")},
			{"Level1", 				ResourceLoader.Load<AudioStream>(MUSIC_PATH + "main_stage_music.mp3")},
			{"MainMenu", 			ResourceLoader.Load<AudioStream>(MUSIC_PATH + "main_menu_music.mp3")},
			{"OniBoss", 			ResourceLoader.Load<AudioStream>(MUSIC_PATH + "boss_stage_music.mp3")},
			{"Victory", 			ResourceLoader.Load<AudioStream>(MUSIC_PATH + "win_screen_music.mp3")}
		};
		
		currentMusic = new AudioStreamPlayer();
		currentMusic.Stream = loadedMusic["MainMenu"];
		currentMusic.VolumeDb = MUSIC_VOLUME_DB;
		this.AddChild(currentMusic);
		
		loadedSounds = new Dictionary<string, Dictionary<string, AudioStream>> 
		{
			{
				"enemy_oni", 
				new Dictionary<string, AudioStream>
				{
					{"damage", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_damage.wav")},
					{"death", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_death.wav")}
				}
			},
			{
				"enemy_oni_boss", 
				new Dictionary<string, AudioStream>
				{	            
					{"damage", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_damage.wav")},
					{"death", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_death.wav")},
					{"death_vaporize", 	ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_death_vaporize.wav")},
					{"drop", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_drop.wav")},
					{"drum-attack", 	ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_drum-attack.wav")},
					{"drum-charge", 	ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_drum-charge.wav")},
					{"laugh", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_laugh.wav")},
					{"phase2-groan", 	ResourceLoader.Load<AudioStream>(SOUND_PATH + "enemy/enemy_oni_boss_groan.wav")}
				}
			},
			{
				"player", 
				new Dictionary<string, AudioStream>
				{
					{"attack", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "player/player_attack_16bit.wav")},
					{"damage", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "player/player_damage.wav")},
					{"jump", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "player/player_jump_16bit.wav")}
				}
			},
			{
				"powerups", 
				new Dictionary<string, AudioStream>
				{
					{"attack-boost", 	ResourceLoader.Load<AudioStream>(SOUND_PATH + "powerups/attack-boost.wav")},
					{"heal", 			ResourceLoader.Load<AudioStream>(SOUND_PATH + "powerups/heal.wav")},
					{"jump-boost", 		ResourceLoader.Load<AudioStream>(SOUND_PATH + "powerups/jump-boost.wav")},
					{"speed-boost", 	ResourceLoader.Load<AudioStream>(SOUND_PATH + "powerups/speed-boost.wav")}
				}
			},
			{
				"user_interface", 
				new Dictionary<string, AudioStream>
				{
					{"quit_button_press", ResourceLoader.Load<AudioStream>(SOUND_PATH + "menu/button_press.wav")},
					{"start_button_press", ResourceLoader.Load<AudioStream>(SOUND_PATH + "menu/button_press2.wav")}
				}
			}
		};
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
