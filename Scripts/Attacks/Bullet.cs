using Godot;
using System;

public class Bullet : KinematicBody2D
{
	[Export]
	private int damage = 1;

	[Export]
	private float speed = 30;

	[Export]
	private float gravity = 0;
	public float Gravity
	{
		get { return gravity; }
		set { gravity = value; }
	}

	[Export]
	private Vector2 impulse = new Vector2(500,750);

	private Vector2 velocity = new Vector2(0,0);
	public Vector2 Heading
	{
		get{ return velocity; }
		set{ velocity = value; }
	}

	[Export]
	private bool destroyOnWall = false;
	
	private Sprite sprite;

	public override void _Ready()
	{
		sprite = (Sprite)GetNode("Sprite");
		KillBulletTimer();
	}

	// Called when the node enters the scene tree for the first time.
	public override void _PhysicsProcess(float delta)
	{
		//direction.x is constant
		//direction.y can be affected by gravity
		//GD.Print("Change = ", gravity * delta);
		//GD.Print("Y = ", velocity.y);
		velocity.y += gravity * delta;

		//Kinematic Collision captures a collision if it occurs during the movement... this is how we damage :)
		//Remember, X = xo + vt + at^2, xo is added on in MoveAndCollide, and direction is currently v + at, so...
		KinematicCollision2D collisionInfo = MoveAndCollide(velocity * delta);

		sprite.FlipH = velocity.x >= 0;

		//We adjust the mask so it can only collide with a selected Actor
		//Could be useful if we want to destroy the bullet on wall
		if (destroyOnWall && collisionInfo?.Collider != null)
		{
			Vanish();
		}
	}

	public void Vanish()
	{
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		GetNode<Area2D>("DamageDealer").SetDeferred("monitoring", false);
		sprite.Visible = false;
		SetPhysicsProcess(false);
	}

	public async void KillBulletTimer()
	{
		await ToSignal(GetTree().CreateTimer(2), "timeout");
		Vanish();
		GD.Print("Final Height: ", GlobalPosition.y);
		CallDeferred("queue_free");
	}

	public void SetGravity(float newGravity = 0)
	{
		gravity = newGravity;
	}

	//Called when the bullet collides with the actor
	public void PlayerCollision(KinematicBody2D collision)
	{
		GD.Print((GetNode<CollisionShape2D>("CollisionShape2D")).Disabled);
		Actor damaged = collision as Actor;
		damaged.TakeDamage(damage, GlobalPosition, impulse);
		Vanish();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
