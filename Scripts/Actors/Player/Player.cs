using Godot;
using System;

// Finite State Machine for the Player
public class Player : Actor
{
	public override void _Ready()
	{
		//Set the character sprite
		base._Ready();

		container.SetState("Idle", new Idle(this));
		container.SetState("Walk", new Walk(this));
		container.SetState("Run", new Run(this));
		container.SetState("Jump", new Jump(this));
		container.SetState("Death", new Death(this));

		/*
		Node states = GetNode("States");
		container.SetState("Idle", (State)states.GetNode("Idle"));
		container.SetState("Walk", (State)states.GetNode("Walk"));
		container.SetState("Jump", (State)states.GetNode("Jump"));
		*/

		state = container.GetState("Idle");
	}

	public override void Die()
	{
		GetTree().ChangeScene("res://Scenes/GameOver.tscn");
	}
	
	public void _on_Area2D_body_entered(object body)
	{
		Node2D RespawnNode = GetNode<Node2D>("/root/World/Camera2D/RespawnPoint");
		GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), GetType().Name, "Damage");
		hp -= 1;
		Position = RespawnNode.GlobalPosition;
	}
}



