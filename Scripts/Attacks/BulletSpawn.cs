using Godot;
using System;

public class BulletSpawn : Attack
{

	//bullet prefab
	[Export]
	private PackedScene bulletPrefab;

	//Which direction bullets will move (set in editor)
	[Export]
	private Vector2 heading = new Vector2(0,0);

	//relevant member variables from Attack
	/*
	Actor host
	Timer time
	*/

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		host = (Actor)GetParent();

		bulletPrefab = GD.Load<PackedScene>("res://Scenes/Prefabs/Bullet.tscn");

		//cooldown timer
		time = (Timer)host.GetNode("AttackCooldown");
		return;
	}

	//Do nothing instead
	public override void _Process(float delta)
	{
		//Do nothing
		return;
	}


	//Spawn the bullet
	//prevAnim is useless in this context
	public override void StartAttack(string prevAnim = "")
	{
		GD.Print("Shooting Projectile");
		
		//Spawn an instance of the bullet with a specific heading
		Bullet bullet = bulletPrefab.Instance() as Bullet;
		bullet.Heading = heading;
		AddChild(bullet);
	}
}
