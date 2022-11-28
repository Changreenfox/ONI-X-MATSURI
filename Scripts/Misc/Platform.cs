using Godot;
using System;

public class Platform : StaticBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		
	}
	
	private void _on_RespawnTimer_timeout()
	{
		Area2D decayArea = GetNodeOrNull<Area2D>("DecayArea");
		decayArea?.SetDeferred("monitoring", true);
		CollisionShape2D hitbox = GetNodeOrNull<CollisionShape2D>("Hitbox");
		hitbox?.SetDeferred("disabled", false);
		GetNode<Sprite>("Sprite").Visible = true;
		
		GD.Print("Platform respawned!!");
	}


	private void _on_Area2D_body_entered(object body)
	{
		GD.Print("Platform is now decaying!!!!!!!!!!!!!!!!!!!!!");
		Area2D decayArea = GetNodeOrNull<Area2D>("DecayArea");
		decayArea?.SetDeferred("monitoring", false);
		Timer decayTimer = GetNodeOrNull<Timer>("DecayTimer");
		decayTimer?.Start();
	}


	private void _on_DecayTimer_timeout()
	{
		Area2D decayArea = GetNodeOrNull<Area2D>("DecayArea");
		decayArea?.SetDeferred("monitoring", false);
		CollisionShape2D hitbox = GetNodeOrNull<CollisionShape2D>("Hitbox");
		hitbox?.SetDeferred("disabled", true);
		GetNode<Sprite>("Sprite").Visible = false;
		
		Timer respawnTimer = GetNodeOrNull<Timer>("RespawnTimer");
		respawnTimer.Start();
	}

	
}
