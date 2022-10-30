using Godot;
using System;

public abstract class PowerUp : KinematicBody2D
{
	protected GameManager gManager;
	protected Area2D area;
	protected Player player;
	protected UIManager Interface;

	[Export]
	protected float boostTime = 3.0f;
	
	[Export]
	protected float gravity = 0f;//40f;
	public float Gravity
	{
		get{ return gravity; }
		set{ gravity = value; CheckGravity(value); }
	}
	
	[Export]
	protected float maxFallSpeed = 1000f;
	
	protected Vector2 velocity;
	protected Vector2 up;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
		//The whole GameObject
		area = GetNode<Area2D>("Area2D");
		player = gManager.PlayerRef;
		Interface = gManager.InterfaceRef;
		velocity = Vector2.Zero;
		up = new Vector2(0, -1);
		CheckGravity(gravity);
	}	
	
	public override void _PhysicsProcess(float delta)
	{
		velocity.y += gravity;
		velocity.y = Mathf.Min(maxFallSpeed, velocity.y);

		velocity = MoveAndSlide(velocity, up);
	}
	
	protected virtual void _on_Area2D_body_entered(object body)
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
												"Powerup",
												GetType().Name
												);
		ActivatePowerUp();

		area.SetDeferred("monitoring", false);
		GetNode<Sprite>("Sprite").Visible = false;
	}

	protected async virtual void ActivatePowerUp() {}
	
	private void CheckGravity(float gravityValue)
	{
		SetPhysicsProcess(((gravityValue == 0) ? false : true));
	}
}
