using Godot;
using System;

public class UIManager : Control
{
	private GameManager gManager;
	private Label scoreLabel;
	
	//private Player PlayerNode;
	private TextureRect Heart1;
	private TextureRect Heart2;
	private TextureRect Heart3;
	
	private Texture HeartFull;
	private Texture HeartHalf;
	private Texture HeartEmpty;
	
	private TextureRect JumpBoost;
	private TextureRect AttackBoost;
	private TextureRect SpeedBoost;

	private Control pauseMenu;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = (GameManager)GetNode("/root/GameManager");
		gManager.InterfaceRef = this;
		
		gManager.Signals.Connect(nameof(SignalManager.UpdatedGameScore), this, nameof(SetScoreDisplay));
		
		//PlayerNode = gManager.PlayerRef;
		Heart1 = GetNode<TextureRect>("Left/Heart1");
		Heart2 = GetNode<TextureRect>("Left/Heart2");
		Heart3 = GetNode<TextureRect>("Left/Heart3");
		
		HeartFull = (Texture)GD.Load("res://assets/sprites/heart_full.png");
		HeartHalf = (Texture)GD.Load("res://assets/sprites/heart_half.png");
		HeartEmpty = (Texture)GD.Load("res://assets/sprites/heart_empty.png");
			
		JumpBoost = GetNode<TextureRect>("Right/VBoxContainer/JumpBoost");
		AttackBoost = GetNode<TextureRect>("Right/VBoxContainer/AttackBoost");
		SpeedBoost = GetNode<TextureRect>("Right/VBoxContainer/SpeedBoost");

		pauseMenu = GetNode<Control>("PauseScreen");
		
		scoreLabel = GetNodeOrNull<HBoxContainer>("Center").GetNodeOrNull<Label>("Label");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		// Set heart sprites based on player health
		if (gManager.PlayerRef?.HP <= 0)
		{
			Heart1.Texture = HeartEmpty;
			Heart2.Texture = HeartEmpty;
			Heart3.Texture = HeartEmpty;
		}
		
		else if (gManager.PlayerRef?.HP <= 2)
		{
			if(gManager.PlayerRef.HP == 1)
				Heart1.Texture = HeartHalf;
			else
				Heart1.Texture = HeartFull;
			Heart2.Texture = HeartEmpty;
			Heart3.Texture = HeartEmpty;
		}
		else if (gManager.PlayerRef?.HP <= 4)
		{
			Heart1.Texture = HeartFull;
			if(gManager.PlayerRef.HP == 3)
				Heart2.Texture = HeartHalf;
			else
				Heart2.Texture = HeartFull;
			Heart3.Texture = HeartEmpty;
		}
		else
		{
			Heart1.Texture = HeartFull;
			Heart2.Texture = HeartFull;
			if(gManager.PlayerRef?.HP == 5)
				Heart3.Texture = HeartHalf;
			else
				Heart3.Texture = HeartFull;
		}
	}
	
	public void Display_Powerup_Icon(string type)
	{
		if (type == "Jump")
			JumpBoost.Visible = true;
		else if (type == "Attack")
			AttackBoost.Visible = true;
		else if (type == "Speed")
			SpeedBoost.Visible = true;
	}
	
	public void Hide_Powerup_Icon(string type)
	{
		if (type == "Jump")
			JumpBoost.Visible = false;
		else if (type == "Attack")
			AttackBoost.Visible = false;
		else if (type == "Speed")
			SpeedBoost.Visible = false;
	}
	
	private void SetScoreDisplay(string scoreValue)
	{
		scoreLabel.Text = scoreValue;
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey) {
			if (eventKey.Pressed == true) {
				switch (eventKey.Scancode) {
					// enter pause state
					case (int)KeyList.P : {
						GetTree().Paused = true;
						pauseMenu.Show();
						break;
					}
					default : {
						break;
					}
				}
			}
		}
	}
}


