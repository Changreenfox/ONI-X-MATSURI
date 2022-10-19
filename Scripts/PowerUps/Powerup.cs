using Godot;
using System;

public abstract class Powerup : Area2D
{
	protected GameManager gManager;
	protected Node parent;
	protected Player player;
	protected UIManager Interface;

	[Export]
	protected float boostTime = 3.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
		//The whole GameObject
		parent = GetParent();
		player = gManager.PlayerRef;
		Interface = gManager.InterfaceRef;
	}	
	
	protected virtual void _on_Area2D_body_entered(object body)
	{
		gManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), 
												"Powerup",
												GetType().Name
												);
		ActivatePowerUp();

		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		parent.GetNode<Sprite>("Sprite").Visible = false;
	}

	protected async virtual void ActivatePowerUp() {}
}
