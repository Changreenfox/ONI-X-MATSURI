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
	private AnimationPlayer animationPlayer;
	
	private Timer respawnTimer;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		decayObjects = new HashSet<object>();
		
		decayDurability = 1.0f;	//Standing on a platform for a total of 5 seconds will cause it to break
		decayArea = GetNodeOrNull<Area2D>("DecayArea");
		hitbox = GetNodeOrNull<CollisionShape2D>("Hitbox");
		sprite = GetNodeOrNull<Sprite>("Sprite");
		
		respawnTimer = GetNodeOrNull<Timer>("RespawnTimer");
		
		animationPlayer = GetNodeOrNull<AnimationPlayer>("AnimationPlayer");
		
		CallDeferred("set_process", false);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		//Should only be processed if >= 1 object is standing on the platform
		if(sprite == null)
		{
			GD.Print("Error with decay: no sprite!");
			return;
		}
		decayDurability -= delta;
		
		if(decayDurability <= 0.7f) sprite.Frame = 1;
		
		if(decayDurability <= 0.4f) sprite.Frame = 2;
		
		if(decayDurability <= 0.0f)
		{
			//Platform breaks
			decayArea?.SetDeferred("monitoring", false);
			hitbox?.SetDeferred("disabled", true);
			sprite.Frame = 3;
			
			animationPlayer?.Play("fading");
			
			CallDeferred("set_process", false);
			respawnTimer.Start();
		}
	}
	
	private void _on_RespawnTimer_timeout()
	{
		decayDurability = 1.0f;
		SetActive(true);
		sprite.Modulate = new Color(255, 255, 255, 255);
		sprite.Frame = 0;
		animationPlayer.Play("default");
		
	}
	
	private void _on_DecayArea_body_entered(KinematicBody2D body)
	{
		decayObjects.Add(body);
		GD.Print(body.Name);
		CallDeferred("set_process", true);
	}

	private void _on_DecayArea_body_exited(object body)
	{
		decayObjects.Remove(body);
		if(decayObjects.Count == 0)
		{
			CallDeferred("set_process", false);
		}
	}
	
	private void SetActive(bool enable)
	{
		decayArea?.SetDeferred("monitoring", enable);
		hitbox?.SetDeferred("disabled", !enable);
		sprite?.SetDeferred("visible", enable);
	}
}
