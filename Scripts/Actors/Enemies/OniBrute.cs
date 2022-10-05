using Godot;
using System;

public class OniBrute : Enemy
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		
		container.SetState("Idle", new EnemyIdle(this));
		container.SetState("Death", new Death(this));

		state = container.GetState("Idle");
		state.Enter();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
