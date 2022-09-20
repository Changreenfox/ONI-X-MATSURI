using Godot;
using System;

public class TextureManager : Node
{
	
	//[Export]
	private static string TEXTURE_PATH = "res://assets/sprites/";
	
	//Format: Dictionary<Entity name, Dictionary<Entity sound name, Sound path>>
	//[Export]
	/*private Dictionary<string, Dictionary<string, string>> texturePaths = 
		new Dictionary<string, Dictionary<string, string>> 
		{
			{
				"enemy_oni", 
				new Dictionary<string, string>
				{
					{"damage", 			(TEXTURE_PATH + "enemy/enemy_oni_damage.wav")},
					{"death", 			(TEXTURE_PATH + "enemy/enemy_oni_death.wav")}
				}
			},
			{
				"enemy_oni_boss", 
				new Dictionary<string, string>
				{	            
					{"damage", 			(TEXTURE_PATH + "enemy/enemy_oni_boss_damage.wav")},
					{"death", 			(TEXTURE_PATH + "enemy/enemy_oni_boss_death.wav")},
					{"death_vaporize", 	(TEXTURE_PATH + "enemy/enemy_oni_boss_death_vaporize.wav")},
					{"drop", 			(TEXTURE_PATH + "enemy/enemy_oni_boss_drop.wav")},
					{"drum-attack", 	(TEXTURE_PATH + "enemy/enemy_oni_boss_drum-attack.wav")},
					{"drum-charge", 	(TEXTURE_PATH + "enemy/enemy_oni_boss_drum-charge.wav")},
					{"laugh", 			(TEXTURE_PATH + "enemy/enemy_oni_boss_laugh.wav")},
					{"phase2-groan", 	(TEXTURE_PATH + "enemy/enemy_oni_boss_groan.wav")}
				}
			},
			{
				"player", 
				new Dictionary<string, string>
				{
					{"attack", 			(TEXTURE_PATH + "player/player_attack_16bit.wav")},
					{"damage", 			(TEXTURE_PATH + "player/player_damage.wav")},
					{"jump", 			(TEXTURE_PATH + "player/player_jump_16bit.wav")}
				}
			},
			{
				"powerups", 
				new Dictionary<string, string>
				{
					{"attack-boost", 	(TEXTURE_PATH + "powerups/attack-boost.wav")},
					{"heal", 			(TEXTURE_PATH + "powerups/heal.wav")},
					{"jump-boost", 		(TEXTURE_PATH + "powerups/jump-boost.wav")},
					{"speed-boost", 	(TEXTURE_PATH + "powerups/speed-boost.wav")}
				}
			},
			{
				"user_interface", 
				new Dictionary<string, string>
				{
					{"quit_button_press", (TEXTURE_PATH + "menu/button_press.wav")},
					{"start_button_press", (TEXTURE_PATH + "menu/button_press2.wav")}
				}
			}
		};
	
	//Format: Dictionary<Entity name, Dictionary<Entity sound name, Sound>>
	//[Export]
	private Dictionary<string, Dictionary<string, AudioStream>> loadedTextures;
	*/

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
