using Godot;
using System;

// Finite State Machine for the Player
public class Player : Actor
{
	private UIManager Interface;
	
	public override void _Ready()
	{
		//Set the character sprite
		base._Ready();
		
		GManager.PlayerRef = this;

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
		
		Interface = gManager.InterfaceRef;
	}

	public override void Die()
	{
		GetTree().ChangeScene("res://Scenes/GameOver.tscn");
	}
	
	public void _on_PitCheck_body_entered(object body)
	{
		if(body == this)
		{
			Node2D RespawnNode = GetNode<Node2D>("/root/World/Camera2D/RespawnPoint");
			GManager.Signals.EmitSignal(nameof(SignalManager.PlaySoundSignal), GetType().Name, "Damage");
			hp -= 1;
			Position = RespawnNode.GlobalPosition;
		}
	}
	
	public void _on_EndWall_body_entered(object body)
	{
		if(body == this) GetTree().ChangeScene("res://Scenes/BossLevel.tscn");
	}
	
	public async void _on_Powerup_pickup(string type)
	{		
		float BoostTime = 3.0f;
		
		if (type == "Health")
		{
			if (HP < 6)
				HP += 1;
		}
		else if (type == "Jump")
		{
			JumpSpeed = 1500;
			Interface.Toggle_Powerup_Icon(type);
			
			await ToSignal(GetTree().CreateTimer(BoostTime), "timeout");
			
			JumpSpeed = 1000;
			Interface.Toggle_Powerup_Icon(type);
		}
		else if (type == "Speed")
		{
			MaxSpeed = 1000;
			Interface.Toggle_Powerup_Icon(type);
			
			await ToSignal(GetTree().CreateTimer(BoostTime), "timeout");
			
			MaxSpeed = 600;
			Interface.Toggle_Powerup_Icon(type);
		}
		else if (type == "Attack")
		{
			// insert what needs to be done to adjust attack damage
			Interface.Toggle_Powerup_Icon(type);
			
			await ToSignal(GetTree().CreateTimer(BoostTime), "timeout");
			
			Interface.Toggle_Powerup_Icon(type);
		}
	}
}
