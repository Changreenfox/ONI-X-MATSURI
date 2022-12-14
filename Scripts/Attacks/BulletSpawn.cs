using Godot;
using System;

public class BulletSpawn : Attack
{

	//bullet prefab
	[Export]
	private PackedScene bulletPrefab;

	//Which direction bullets will move (set in editor)
	[Export]
	private Vector2 heading = Vector2.Zero;
	public Vector2 Heading
	{
		get { return heading; }
		set { heading = value; }
	}

	[Export]
	private float gravity = 0;
	public float Grav
	{
		get { return gravity; }
		set { gravity = value; }
	}

	//relevant member variables from Attack
	/*
	Actor host
	Timer time
	*/

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		FindHost();

		//cooldown timer
		time = (Timer)host.GetNode("AttackCooldown");

		if(bulletPrefab == null)
			bulletPrefab = GD.Load<PackedScene>("res://Scenes/Prefabs/ThrowBullet.tscn");

		//Don't need to process anything
		SetProcess(false);
	}


	//Spawn the bullet
	//prevAnim is useless in this context
	public override void StartAttack(string prevAnim = "")
	{
		//Spawn an instance of the bullet with a specific heading
		Bullet bullet = bulletPrefab.Instance() as Bullet;
		bullet.Heading = heading;
		bullet.Gravity = gravity;
		AddChild(bullet);
		Vector2 pos = bullet.GlobalPosition;
		bullet.SetAsToplevel(true);
		bullet.GlobalPosition = pos;
	}
}
