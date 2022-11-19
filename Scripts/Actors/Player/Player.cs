using Godot;
using System;

// Finite State Machine for the Player
public class Player : Actor
{
	[Export]
	private int maxHealth = 6;
	public int MaxHealth
	{
		get{ return maxHealth; }
	}	
	
	public override void _EnterTree()
	{
		base._EnterTree();
		GManager.Signals.EmitSignal(nameof(SignalManager.PlayerLoaded),
									this
									);
		//gManager.PlayerRef = this;
	}

	public override void _Ready()
	{
		//Set the character sprite
		base._Ready();
		
		GManager.Signals.EmitSignal(nameof(SignalManager.PlayerLoaded),
									this
									);
		//GManager.PlayerRef = this;

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
		state.Enter();
	}

	public override void Die()
	{
		GetTree().ChangeScene("res://Scenes/GameOver.tscn");
	}

	public override void Reset(int damage = 0, Node2D RespawnNode = null)
	{
		if(damage != 0)
		{
			PlaySound("Damage");
			hp -= damage;
			FlashColor(0.3f, toFlash);
		}
		if(RespawnNode != null)
		{
			Position = RespawnNode.GlobalPosition;
			velocity.y = 0;
		}
	}
}
