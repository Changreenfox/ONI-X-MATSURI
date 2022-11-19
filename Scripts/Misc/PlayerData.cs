using Godot;
using System;

public class PlayerData : Node
{
	/*
	This class is used to save the Player's data to persist between scenes
	-HP
	-Status effects (PowerUps)
	*/
	
	/*
	This class is not used for creating game saves
	This will not store all data relevant to the Player (i.e., world position, velocity, etc.)
	*/
	
	private int healthPoints = 0;
	public int HealthPoints
	{
		get{ return healthPoints; }
	}
	
	private float attackPowerUpDuration = 0;
	public float AttackPowerUpDuration
	{
		get{ return attackPowerUpDuration; }
	}
	
	private float jumpPowerUpDuration = 0;
	public float JumpPowerUpDuration
	{
		get{ return jumpPowerUpDuration; }
	}
	
	private float speedPowerUpDuration = 0;
	public float SpeedPowerUpDuration
	{
		get{ return speedPowerUpDuration; }
	}
	
	
	public PlayerData(Player player)
	{
		healthPoints = player.HP;
		attackPowerUpDuration = 0;
		jumpPowerUpDuration = 0;
		speedPowerUpDuration = 0;
	}
}
