using Godot;
using System;
using System.Collections.Generic;	//HashSet

public class Platform : StaticBody2D
{
	private HashSet<object> decayObjects;
	
	private float decayDurability;
	private Area2D decayArea;
	private CollisionShape2D hitbox;
	private Sprite sprite;
	
	private Timer respawnTimer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		decayObjects = new HashSet<object>();
		
		decayDurability = 3.0f;	//Standing on a platform for a total of 5 seconds will cause it to break
		decayArea = GetNodeOrNull<Area2D>("DecayArea");
		hitbox = GetNodeOrNull<CollisionShape2D>("Hitbox");
		sprite = GetNodeOrNull<Sprite>("Sprite");
		
		respawnTimer = GetNodeOrNull<Timer>("RespawnTimer");
		
		CallDeferred("set_process", false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		//Should only be processed if >= 1 object is standing on the platform
		decayDurability -= delta;
		if(decayDurability <= 0.0f)
		{
			//Platform breaks
			decayArea?.SetDeferred("monitoring", false);
			hitbox?.SetDeferred("disabled", true);
			sprite?.SetDeferred("visible", false);
			
			CallDeferred("set_process", false);
			
			respawnTimer.Start();
		}
	}
	
	private void _on_RespawnTimer_timeout()
	{
		decayDurability = 3.0f;
		
		decayArea?.SetDeferred("monitoring", true);
		hitbox?.SetDeferred("disabled", false);
		sprite?.SetDeferred("visible", true);
		
		GD.Print("Platform respawned!!");
	}
	
	private void _on_DecayArea_body_entered(object body)
	{
		decayObjects.Add(body);
		CallDeferred("set_process", true);
		GD.Print("Platform is now decaying!!!!!!!!!!!!!!!!!!!!!");
	}

	private void _on_DecayArea_body_exited(object body)
	{
		decayObjects.Remove(body);
		if(decayObjects.Count == 0)
		{
			CallDeferred("set_process", false);
		}
	}
}
