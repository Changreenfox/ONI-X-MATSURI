using Godot;
using System;

public class DrumBounce : Area2D
{
	protected GameManager gManager;
	protected Player player;
	protected AnimationPlayer animation;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		gManager = GetNode<GameManager>("/root/GameManager");
		player = gManager.PlayerRef;
		animation = GetParent().GetNode<AnimationPlayer>("AnimationPlayer");
	}

	private void _on_Area2D_body_entered(object body)
	{
		if(player.GlobalPosition[1] < GlobalPosition[1]){//if the body is entered from on-top
			if(player.Velocity[1] > 0){
				Vector2 newVelocity = player.Velocity;
				newVelocity[1] = -1500;
				player.Velocity = newVelocity;
				animation.Play("Bounce");
			}
		}
	}

}
