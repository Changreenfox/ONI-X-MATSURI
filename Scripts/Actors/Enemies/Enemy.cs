using Godot;
using System;

public class Enemy : Actor
{
	//Enemy Functionality
	public override void _Ready()
	{
			base._Ready();

			container.SetState("Idle", new EnemyIdle(this));
			container.SetState("Death", new Death(this));

			state = container.GetState("Idle");
			state.Enter();
	}
}       
