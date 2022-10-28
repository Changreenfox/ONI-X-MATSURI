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

	private Vector2 direction = new Vector2(0,0);
	public Vector2 Heading
	{
		get{ return direction; }
		set{ direction = value; }
	}
	
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
		direction.y += gravity * delta;

		//Kinematic Collision captures a collision if it occurs during the movement... this is how we damage :)
		KinematicCollision2D collisionInfo = MoveAndCollide(direction.Normalized() * speed);

		sprite.FlipH = direction.x >= 0;

		//We adjust the mask so it can only collide with a selected Actor
		if (collisionInfo?.Collider != null)
		{
			Actor damaged = (Actor)collisionInfo.Collider;
			damaged.TakeDamage(damage, GlobalPosition, impulse);
			((CollisionShape2D)GetNode("CollisionShape2D")).SetDeferred("disabled", true);
			sprite.SetDeferred("Visible", false);
			SetPhysicsProcess(false);
		}
	}

	public async void KillBulletTimer()
	{
		await ToSignal(GetTree().CreateTimer(4), "timeout");
		CallDeferred("queue_free");
	}

	public void SetGravity(float newGravity = 0)
	{
		gravity = newGravity;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
