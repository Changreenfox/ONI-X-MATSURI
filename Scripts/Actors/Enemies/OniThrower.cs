using Godot;
using System;

public class OniThrower : Enemy
{
	//BulletSpawner is stored in host.attacks[0]
	private Actor player;
	private BulletSpawn bs;
	private float timeFalling = 1f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		alertArea = GetNode<Area2D>("Alert");
		lostArea = GetNode<Area2D>("Lost");
		
		scoreValue = 2;
		
		//Function similarly to OniBrute, but doesn't have to move to continuously attack
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Alert", new Alert(this, "Throw"));
		container.SetState("Lost", new Lost(this));
		container.SetState("Death", new Death(this));
		container.SetState("Throw", new Throw(this));
		container.SetState("RunAway", new RunAway(this));

		state = container.GetState("Idle");
		state.Enter();
	}

	public override void HandleAlert(Area2D player)
	{
		alertArea.SetDeferred("monitoring", false);
		lostArea.SetDeferred("monitoring", true);
		ChangeState("Alert");
	}

	public override void HandleLost(Area2D player)
	{
		alertArea.SetDeferred("monitoring", true);
		lostArea.SetDeferred("monitoring", false);
		ChangeState("Lost");
	}

	private async void Shoot()
	{
		player = gManager.PlayerRef;
		//This is guaranteed based on current scene hierarchy
		bs = (BulletSpawn)attacks[0];

		//Shoot
		Vector2 target = player.GlobalPosition;
		//target.x *= 0.95f;
		//Want to shoot just short of the player's current position,

		Vector2 heading = Vector2.Zero;
		//Want to reach the player, as if he were equal level, in 2s
		//X acceleration is 0, so x = vt + c, x is playerGlobal, c is Global, t is given, so
		//x = vt, and t = 2, so v = (x - c)/t
		heading.x = (target.x - bs.GlobalPosition.x) * 0.95f / timeFalling;
		if(heading.x < 0)
			heading.x = Mathf.Min(-100f, heading.x);
		else
			heading.x = Mathf.Max(heading.x, 100f);

		//We want to crash into the player after timeFalling seconds
		//y = 1/2*gt^2 + vt + c, so v = (y - c - 1/2*gt^2) / t, where y is target - pos, c is pos, and g is in bulletSpawner
		//Remember that up is negative in this world
		heading.y = -1 * (bs.GlobalPosition.y - target.y - -0.5f * bs.Grav * timeFalling * timeFalling) / timeFalling;

		//This should be shown in Attack
		bs.Heading = heading;
		Attack(0);

		//Wait until the attack animation is complete
		await ToSignal(animator, "animation_finished");

		ChangeState("RunAway");
	}
}

